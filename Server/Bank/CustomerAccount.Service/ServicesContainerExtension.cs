

using CustomerAccount.Service;
using CustomerAccount.Service.Interfaces;
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
            services.AddScoped<ICustomerStorage, CustomerStorage>();
            services.AddScoped<IAccountStorage, AccountStorage>();
            services.AddScoped<IOperationHistoryStorage, OperationHistoryStorage>();
            services.AddScoped<IEmailVerificationStorage, EmailVerificationStorage>();
            services.AddScoped<IAuthorizationFuncs , AuthorizationFuncs>();
            services.AddDbContextFactory<CustomerAccountDbContext>(opt => opt.UseSqlServer(connection));
       
        }
    }
}
