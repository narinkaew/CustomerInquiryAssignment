using CustomerInquiry.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerInquiry.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDBContext _dbContext;

        public CustomerRepository(CustomerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieve all customers information
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customers>> ListAsync()
        {
            return await _dbContext.Customers.Include(x => x.Transactions).ToListAsync();
        }

        /// <summary>
        /// Search customer detail with customerId and/or email
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="email"></param>
        /// <returns>Specific customer detail with payment history</returns>
        public async Task<Customers> GetByIdAndEmailAsync(decimal? customerId, string email)
        {
            return await _dbContext.Customers.Include(x => x.Transactions)
                            .Where(x =>
                                (!customerId.HasValue || x.CustomerId.Equals(customerId.Value))
                                && (string.IsNullOrEmpty(email) || x.ContactEmail.Equals(email, StringComparison.InvariantCultureIgnoreCase))
                            )
                            .SingleOrDefaultAsync();
        }
    }
}
