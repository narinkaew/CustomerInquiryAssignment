using CustomerInquiry.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerInquiry.Api
{
    public static class DependencyInjectionConfig
    {
        public static void AddScope(IServiceCollection services)
        {
            #region Repository

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            #endregion
        }
    }
}
