using CustomerInquiry.Commons;
using CustomerInquiry.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class MockDbContext
    {
        /// <summary>
        /// Mocking with in memory database for unit test
        /// Initial 4 customers
        ///    1. Customer with no transaction
        ///    2. Customer with 1 transaction
        ///    3. Customer with 2 transactions
        ///    4. Customer with multiple transactions
        /// </summary>
        /// <returns></returns>
        public CustomerDBContext DbContext()
        {
            DbContextOptions<CustomerDBContext> options;
            var builder = new DbContextOptionsBuilder<CustomerDBContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            //builder.EnableSensitiveDataLogging();
            options = builder.Options;
            var context = new CustomerDBContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            CustomerInit(context);

            context.SaveChanges();
            return context;
        }

        private void CustomerInit(CustomerDBContext context)
        {
            var customers = new List<Customers>
            {
                new Customers()
                {
                    CustomerId = 99999,
                    CustomerName = "Mr. No Transaction",
                    ContactEmail = "no.transaction@2c2p.com",
                    MobileNo = "0891234567"
                },
                new Customers()
                {
                    CustomerId = 11111,
                    CustomerName = "Mr. One Transaction",
                    ContactEmail = "one.transaction@2c2p.com",
                    MobileNo = "0811234567",
                    Transactions = new List<Transactions>()
                    {
                        new Transactions()
                        {
                            TransactionDate = new DateTime(2019,7,1, 1,11,11),
                            Amout = 100.11M,
                            CurrencyCode = CurrencyCode.UnitedStates,
                            Status = TransactionStatus.Success
                        }
                    }
                },
                new Customers()
                {
                    CustomerId = 22222,
                    CustomerName = "Mr. Two Transaction",
                    ContactEmail = "two.transaction@2c2p.com",
                    MobileNo = "0821234567",
                    Transactions = new List<Transactions>()
                    {
                        new Transactions()
                        {
                            TransactionDate = new DateTime(2019,7,2, 2,21,21),
                            Amout = 201.22M,
                            CurrencyCode = CurrencyCode.UnitedStates,
                            Status = TransactionStatus.Success
                        },
                        new Transactions()
                        {
                            TransactionDate = new DateTime(2019,7,2, 2,22,22),
                            Amout = 202.22M,
                            CurrencyCode = CurrencyCode.Thai,
                            Status = TransactionStatus.Failed
                        }
                    }
                },
                new Customers()
                {
                    CustomerId = 33333,
                    CustomerName = "Mr. Many Transaction",
                    ContactEmail = "many.transaction@2c2p.com",
                    MobileNo = "0831234567",
                    Transactions = new List<Transactions>()
                    {
                        new Transactions()
                        {
                            TransactionDate = new DateTime(2019,7,3, 3,31,31),
                            Amout = 301.33M,
                            CurrencyCode = CurrencyCode.UnitedStates,
                            Status = TransactionStatus.Success
                        },
                        new Transactions()
                        {
                            TransactionDate = new DateTime(2019,7,3, 3,32,32),
                            Amout = 302.33M,
                            CurrencyCode = CurrencyCode.Thai,
                            Status = TransactionStatus.Failed
                        },
                        new Transactions()
                        {
                            TransactionDate = new DateTime(2019,7,3, 3,33,33),
                            Amout = 303.33M,
                            CurrencyCode = CurrencyCode.Japan,
                            Status = TransactionStatus.Canceled
                        }
                    }
                }
            };

            customers.ForEach(x => context.Customers.AddAsync(x));
        }
    }
}
