using CustomerInquiry.Api.Controllers;
using CustomerInquiry.Commons;
using CustomerInquiry.Models.Entity;
using CustomerInquiry.Repositories;
using CustomerInquiry.Services;
using CustomerInquiry.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Xunit;

namespace UnitTests
{
    public class CustomerControllerTest
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerDBContext _dbContext;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly CustomerController _customerController;

        public CustomerControllerTest()
        {
            _logger = new MockLogger<CustomerController>();
            _dbContext = new MockDbContext().DbContext();
            _customerRepository = new CustomerRepository(_dbContext);
            _customerService = new CustomerService(_customerRepository);
            _customerController = new CustomerController(_customerService, _logger);
        }

        [Fact]
        public async void GetById_ReturnBadRequest()
        {
            // arrange
            InquiryRequest req = new InquiryRequest()
            {
                CustomerID = null,
                Email = null
            };

            // act
            var badRequestResult = await _customerController.Inquiry(req);

            // assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
            Assert.Equal(ValidationMessage.NoInquiryCriteria, (badRequestResult as BadRequestObjectResult).Value);
        }
        
        [Fact]
        public async void GetById_ReturnNotFound()
        {
           // arrange
           InquiryRequest req = new InquiryRequest()
           {
               CustomerID = 12345
           };

            // act
            var notFoundResult = await _customerController.Inquiry(req);

            // assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async void GetById_ReturnOk()
        {
            // arrange
            InquiryRequest req = new InquiryRequest()
            {
                CustomerID = 11111
            };

            // act
            var okResult = await _customerController.Inquiry(req) as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<InquiryResponse>(okResult.Value);
            Assert.Equal("Mr. One Transaction", (okResult.Value as InquiryResponse).Name);
        }
    }
}
