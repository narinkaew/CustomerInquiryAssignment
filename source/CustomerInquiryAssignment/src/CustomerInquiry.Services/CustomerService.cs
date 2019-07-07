using CustomerInquiry.Commons;
using CustomerInquiry.Repositories;
using CustomerInquiry.ViewModels;
using System;
using System.Threading.Tasks;

namespace CustomerInquiry.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<InquiryResponse> InquiryAsync(InquiryRequest req)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAndEmailAsync(req.CustomerID, req.Email);
                return customer.Convert();
            }
            catch(Exception ex)
            {
                throw new InquiryException(ex.Message);
            }
        }
    }
}
