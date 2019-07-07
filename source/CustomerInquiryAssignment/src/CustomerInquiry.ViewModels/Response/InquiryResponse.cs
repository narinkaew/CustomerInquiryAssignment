using System.Collections.Generic;

namespace CustomerInquiry.ViewModels
{
    public class InquiryResponse
    {
        public decimal CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public IEnumerable<TransactionResponse> Transactions { get; set; }
    }
}
