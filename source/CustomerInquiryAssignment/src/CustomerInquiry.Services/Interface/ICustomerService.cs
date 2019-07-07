using CustomerInquiry.ViewModels;
using System.Threading.Tasks;

namespace CustomerInquiry.Services
{
    public interface ICustomerService
    {
        Task<InquiryResponse> InquiryAsync(InquiryRequest req);
    }
}
