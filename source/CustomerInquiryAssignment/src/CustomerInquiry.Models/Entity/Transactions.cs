using System;
using System.Collections.Generic;

namespace CustomerInquiry.Models.Entity
{
    public partial class Transactions
    {
        public Guid TransactionId { get; set; }
        public decimal CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amout { get; set; }
        public string CurrencyCode { get; set; }
        public string Status { get; set; }

        public virtual Customers Customer { get; set; }
    }
}
