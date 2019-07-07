using CustomerInquiry.Models.Entity;
using CustomerInquiry.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CustomerInquiry.Services
{
    public static class CustomerMapping
    {
        public static InquiryResponse Convert(this Customers model)
        {
            if (model == null)
                return null;

            return new InquiryResponse()
            {
                CustomerID = model.CustomerId,
                Name = model.CustomerName,
                Email = model.ContactEmail,
                Mobile = model.MobileNo,
                Transactions = model.Transactions.Convert()
            };
        }

        public static IEnumerable<TransactionResponse> Convert(this IEnumerable<Transactions> models)
        {
            if (models == null)
                return new List<TransactionResponse>();

            List<TransactionResponse> viewModels = new List<TransactionResponse>();

            foreach (var model in models)
            {
                viewModels.Add(model.Convert());
            }

            // Order by transaction by date
            viewModels = viewModels.OrderBy(x => x.Date).ToList();

            // Running transaction id
            viewModels.ForEach(x => x.ID = viewModels.IndexOf(x) + 1);

            return viewModels;
        }

        public static TransactionResponse Convert(this Transactions model)
        {
            if (model == null)
                return null;

            return new TransactionResponse()
            {
                Date = model.TransactionDate.ToString("dd/MM/yyyy HH:mm"),
                Amount = model.Amout,
                Currency = model.CurrencyCode,
                Status = model.Status
            };
        }
    }
}
