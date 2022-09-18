
using NServiceBus;
using Transaction.Api.Midllewares;
using Transaction.Service;
using Transaction.Service.Interfaces;
using Transaction.Services.Extensions;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var databaseConnection = builder.Configuration.GetConnectionString("DatabaseConnectionShira");

#region NServiceBus configurations


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

builder.Services.AddScoped<ITransactionService, TransactionService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
