using CustomerInquiry.Commons;
using System.ComponentModel.DataAnnotations;

namespace CustomerInquiry.ViewModels
{
    public class InquiryRequest
    {
        [Range(0, 9999999999, ErrorMessage = ValidationMessage.InvalidCustomerID)]
        public decimal? CustomerID { get; set; }

        [EmailAddress(ErrorMessage = ValidationMessage.InvalidEmail)]
        [MaxLength(25, ErrorMessage = ValidationMessage.InvalidEmail)]
        public string Email { get; set; }
    }
}
