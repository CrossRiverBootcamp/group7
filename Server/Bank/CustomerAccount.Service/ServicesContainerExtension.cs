

using CustomerAccount.Storage;
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerAccount.Services.Extensions
{
    public static class ServicesContainerExtension
    {
        public static void AddServiceExtension(this IServiceCollection services, string connection)
        {
            //builder.Services.AddScoped<ICustomerStorage, CustomerStorage>();

            services.AddScoped<ICustomerStorage, CustomerStorage>();
            services.AddScoped<IAccountStorage, AccountStorage>();
            services.AddDbContextFactory<BankDbContext>(opt => opt.UseSqlServer(connection));
        }
    }
}
