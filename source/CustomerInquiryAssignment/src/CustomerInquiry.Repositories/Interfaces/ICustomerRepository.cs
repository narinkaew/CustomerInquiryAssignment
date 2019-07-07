using CustomerInquiry.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerInquiry.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customers>> ListAsync();
        Task<Customers> GetByIdAndEmailAsync(decimal? customerId, string email);
    }
}
