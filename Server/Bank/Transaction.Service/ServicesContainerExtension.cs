
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage;
using Transaction.Storage.Entites;
using Transaction.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Transaction.Services.Extensions;

public static class ServicesContainerExtension
{
    public static void AddServiceExtension(this IServiceCollection services, string connection)
    {
        services.AddScoped<ITransactionStorage, TransactionStorage>();
        services.AddDbContextFactory<TransactionDbContext>(opt => opt.UseSqlServer(connection));
    }
}
