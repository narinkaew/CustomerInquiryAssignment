using System;
using System.Collections.Generic;

namespace CustomerInquiry.Models.Entity
{
    public partial class Customers
    {
        public Customers()
        {
            Transactions = new HashSet<Transactions>();
        }

        public decimal CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactEmail { get; set; }
        public string MobileNo { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
