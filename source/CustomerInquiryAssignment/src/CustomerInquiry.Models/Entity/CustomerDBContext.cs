using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerInquiry.Models.Entity
{
    public partial class CustomerDBContext : DbContext
    {
        public CustomerDBContext()
        {
        }

        public CustomerDBContext(DbContextOptions<CustomerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

        private readonly string _connectionString;

        public CustomerDBContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("ConnectionString can't be empty");
            }

            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                builder.UseSqlServer(_connectionString);
                base.OnConfiguring(builder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.Property(e => e.TransactionId)
                    .HasColumnName("TransactionID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amout).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Transactions_Customers");
            });
        }
    }
}
