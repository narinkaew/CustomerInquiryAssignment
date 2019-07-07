using System;

namespace CustomerInquiry.ViewModels
{
    public class TransactionResponse
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
    }
}
