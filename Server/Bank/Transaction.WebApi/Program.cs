
using NServiceBus;
using Transaction.Api.Midllewares;
using Transaction.Service;
using Transaction.Service.Interfaces;
using Transaction.Services.Extensions;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var databaseConnection = builder.Configuration.GetConnectionString("DatabaseConnection");


#region NServiceBus configurations

var NSBConnection = builder.Configuration.GetConnectionString("NSBConnection");
var queueName = builder.Configuration.GetSection("Queues:AccountAPIQueue:Name").Value;
var rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQ");

builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration(queueName);

    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();

    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
    connectionBuilder: () =>
    {
        return new SqlConnection(NSBConnection);
    });
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    
    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);

    var conventions = endpointConfiguration.Conventions();
    conventions.DefiningEventsAs(type => type.Namespace == "NSB.Event");
    conventions.DefiningCommandsAs(type => type.Namespace == "NSB.Command");
    return endpointConfiguration;
});
#endregion


// Add services to the container.
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddServiceExtension(databaseConnection);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHandlerErrorsMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
