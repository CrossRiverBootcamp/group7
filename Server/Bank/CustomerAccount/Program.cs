using CustomerAccount.Api.Midllewares;
using CustomerAccount.Service;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Services.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var databaseConnection = builder.Configuration.GetConnectionString("DatabaseConnectionZipi");


#region NServiceBus configurations

var rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQ");
var queueName = builder.Configuration.GetSection("Queues:AccountAPIQueue:Name").Value;


builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration(queueName);

    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();

    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
    connectionBuilder: () =>
   {
        return new SqlConnection(databaseConnection);
    });

    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    dialect.Schema("NSB");

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);

    var conventions = endpointConfiguration.Conventions();
    conventions.DefiningEventsAs(type => type.Namespace == "NSB.Event");
    conventions.DefiningCommandsAs(type => type.Namespace == "NSB.Command");
    
    return endpointConfiguration;
});
#endregion

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOperationHistoryService,OperationHistoryService>();
builder.Services.AddScoped<ISendEmail, SendEmail>();
builder.Services.AddScoped<IEmailVerificationService, EmailVerificationService>();

builder.Services.AddServiceExtension(databaseConnection);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHandlerErrorsMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => {
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
