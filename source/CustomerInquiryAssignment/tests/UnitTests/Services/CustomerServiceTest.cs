using CustomerInquiry.Models.Entity;
using CustomerInquiry.Repositories;
using CustomerInquiry.Services;
using CustomerInquiry.ViewModels;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class CustomerServiceTest
    {
        private readonly CustomerDBContext _dbContext;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;

        public CustomerServiceTest()
        {
            _dbContext = new MockDbContext().DbContext();
            _customerRepository = new CustomerRepository(_dbContext);
            _customerService = new CustomerService(_customerRepository);
        }

        [Fact]
        public async void GetById_WhenHaveData()
        {
            // arrange
            InquiryRequest req = new InquiryRequest()
            {
                CustomerID = 11111
            };

            // act
            var response = await _customerService.InquiryAsync(req);

            // assert
            Assert.NotNull(response);
            Assert.Equal("Mr. One Transaction", response.Name);
            Assert.Single(response.Transactions);
        }

        [Fact]
        public async void GetByEmail_WhenHaveData()
        {
            // arrange
            InquiryRequest req = new InquiryRequest()
            {
                Email = "two.transaction@2c2p.com"
            };

            // act
            var response = await _customerService.InquiryAsync(req);

            // assert
            Assert.NotNull(response);
            Assert.Equal("Mr. Two Transaction", response.Name);
            Assert.Equal(2, response.Transactions.Count());
        }

        [Fact]
        public async void GetByIdAndEmail_WhenHaveData()
        {
            // arrange
            InquiryRequest req = new InquiryRequest()
            {
                CustomerID = 99999,
                Email = "no.transaction@2c2p.com"
            };

            // act
            var response = await _customerService.InquiryAsync(req);

            // assert
            Assert.NotNull(response);
            Assert.Equal("Mr. No Transaction", response.Name);
            Assert.Empty(response.Transactions);
        }

        [Theory(DisplayName = "Return null if not found")]
        [InlineData(12345, null)]
        [InlineData(null, "unknown@mail.com")]
        [InlineData(9876543, "hello@mail.com")]
        [InlineData(1234567890123, null)]
        public async void Return_null_if_notfound(long? customerID, string email)
        {
            // arrange
            InquiryRequest req = new InquiryRequest()
            {
                CustomerID = customerID,
                Email = email
            };

            // act
            var response = await _customerService.InquiryAsync(req);

            // assert
            Assert.Null(response);
        }
    }
}
