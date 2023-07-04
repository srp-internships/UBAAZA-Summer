using System;
using System.Data.Entity;
using System.Security.Cryptography;

namespace Stock_
{
    public class My_DbContext : DbContext
    {
        public My_DbContext() : base("DbConnectionString") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Sales>()
                .HasRequired(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<Arrivals>()
              .HasRequired(a => a.Supplier)
              .WithMany(s => s.Arrivals)
              .HasForeignKey(s => s.SupplierId);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Sales> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Arrivals> Arrivals { get; set; }
    }
}
