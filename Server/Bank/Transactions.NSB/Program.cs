using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSB.Command;
using NServiceBus;
using NServiceBus.Logging;
using System.Data.SqlClient;
using Transaction.Service;
using Transaction.Service.Interfaces;
using Transaction.Services.Extensions;

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

        //???????????למה צריך?
        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddServiceExtension(config.GetConnectionString("NSBConnectionShira"));
        containerSettings.ServiceCollection.AddScoped<ITransactionService, TransactionService>();
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(config.GetConnectionString("NSBConnectionShira"));
            });

        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
      
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

