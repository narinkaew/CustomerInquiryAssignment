using CustomerInquiry.Models.Entity;
using CustomerInquiry.Repositories;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class CustomerRepositoryTest
    {
        private CustomerDBContext _dbContext;
        private ICustomerRepository _customerRepository;

        [Fact(DisplayName = "Get all customers")]
        public async void List_all_customers()
        {
            // arrange
            _customerRepository = Initial();

            // act
            var allCustomers = await _customerRepository.ListAsync();

            // assert
            Assert.Equal(4, allCustomers.Count());

            // select Mr. No Transaction
            var customerNoTransaction = allCustomers.Where(x => x.CustomerId == 99999).FirstOrDefault();
            Assert.Equal("no.transaction@2c2p.com", customerNoTransaction.ContactEmail);
            Assert.Empty(customerNoTransaction.Transactions);

            // select Mr. One transaction
            var customerOneTransaction = allCustomers.Where(x => x.CustomerId == 11111).FirstOrDefault();
            Assert.Equal("one.transaction@2c2p.com", customerOneTransaction.ContactEmail);
            Assert.NotEmpty(customerOneTransaction.Transactions);
            Assert.Equal(1, customerOneTransaction.Transactions.Count);
            Assert.Equal(100.11M, customerOneTransaction.Transactions.FirstOrDefault().Amout);
        }
        
        [Fact(DisplayName = "GetById Mr. Multiple Transaction")]
        public async void GetById_MrMultipleTransaction()
        {
            // arrange
            _customerRepository = Initial();
            decimal customerId = 33333;
            string email = null;

            // act
            var customer = await _customerRepository.GetByIdAndEmail(customerId, email);

            // assert
            Assert.Equal("multiple.transaction@2c2p.com", customer.ContactEmail);
            Assert.NotEmpty(customer.Transactions);
            Assert.Equal(3, customer.Transactions.Count);
            Assert.Equal(301.33M, customer.Transactions.ElementAt(0).Amout);
            Assert.Equal(302.33M, customer.Transactions.ElementAt(1).Amout);
            Assert.Equal(303.33M, customer.Transactions.ElementAt(2).Amout);
        }

        private ICustomerRepository Initial()
        {
            _dbContext = new MockDbContext().DbContext();
            return new CustomerRepository(_dbContext);
        }
    }
}
