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
        //builder.Services.AddServiceExtension(builder.Configuration.GetConnectionString("Shira"));

        var databaseConnection = "server=Shira; database=Bank.Transaction;Trusted_Connection=True";
        var rabbitMQConnection = "host=localhost";

        //var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(UpdateAccount), destination: "CustomerAccount");


        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(databaseConnection);
            });

        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        var subscriptions = persistence.SubscriptionSettings();
        subscriptions.CacheFor(TimeSpan.FromMinutes(1));
        dialect.Schema("dbo");

        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace == "NSB.Command");
        conventions.DefiningEventsAs(type => type.Namespace == "NSB.Event");

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}

