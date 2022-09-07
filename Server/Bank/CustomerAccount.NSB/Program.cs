using NServiceBus;
using NServiceBus.Logging;
using System.Data.SqlClient;

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "CustomerAccount";

        var endpointConfiguration = new EndpointConfiguration("CustomerAccount");

        var databaseConnection = "server=DESKTOP-QM3UF42; database=BankProject.Transaction;Trusted_Connection=True";
        var rabbitMQConnection = "host=localhost";

        //var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());

        //containerSettings.ServiceCollection.AddDbContextFactory<TrackingContext>(opt => opt.UseSqlServer(databaseConnection));

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

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
        conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
        conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}
