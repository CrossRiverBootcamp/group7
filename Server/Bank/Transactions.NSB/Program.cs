using Microsoft.Extensions.Configuration;
using NSB.Command;
using NServiceBus;
using NServiceBus.Logging;
using System.Data.SqlClient;


public class Program
{
    static ILog log = LogManager.GetLogger<Program>();
    static async Task Main()
    {
        Console.Title = "Transaction";

        var endpointConfiguration = new EndpointConfiguration("Transaction");

        var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(config.GetConnectionString("NSBConnectionShira"));
            });

        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();

        //var recoverability = endpointConfiguration.Recoverability();
        //recoverability.Immediate(
        //    immediate =>
        //    {
        //        immediate.NumberOfRetries(0);
        //    });
        //recoverability.Delayed(
        //    delayed =>
        //    {
        //        delayed.NumberOfRetries(0);

        //    });

        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace == "NSB.Command");
        conventions.DefiningEventsAs(type => type.Namespace == "NSB.Event");

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(config.GetConnectionString("rabbitMQ"));
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(UpdateAccount), destination: "CustomerAccount");

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}

