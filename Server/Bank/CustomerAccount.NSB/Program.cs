using CustomerAccount.Service;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Services.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;
using System.Data.SqlClient;
using Transaction.Service;

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "CustomerAccount";

        var endpointConfiguration = new EndpointConfiguration("CustomerAccount");

        var databaseConnection = "server=SHIRA; database=Bank.Transaction;Trusted_Connection=True";
        var rabbitMQConnection = "host=localhost";

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddServiceExtension(databaseConnection);
        containerSettings.ServiceCollection.AddScoped<IAccountService, AccountService>();
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));

        //var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        //containerSettings.ServiceCollection.AddDBContextService(databaseConnection);
        //containerSettings.ServiceCollection.AddDIServicesNSB();
        //containerSettings.ServiceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

        //var conventions = endpointConfiguration.Conventions();
        //conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
        //conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}
