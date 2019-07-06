using CustomerInquiry.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CustomerInquiry.Api
{
    public static class DBContextConfig
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetSection("Environment").Value == "local")
            {
                services.AddDbContext<CustomerDBContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            }
            else
            {
                services.AddDbContext<CustomerDBContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("CustomerConnection")));
            }
        }
    }
}
