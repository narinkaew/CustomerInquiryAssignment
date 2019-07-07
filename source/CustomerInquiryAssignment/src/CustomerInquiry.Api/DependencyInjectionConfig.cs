using CustomerInquiry.Repositories;
using CustomerInquiry.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerInquiry.Api
{
    public static class DependencyInjectionConfig
    {
        public static void AddScope(IServiceCollection services)
        {
            #region Service

            services.AddScoped<ICustomerService, CustomerService>();

            #endregion

            #region Repository

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            #endregion
        }
    }
}
