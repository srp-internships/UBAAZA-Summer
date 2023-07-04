namespace Stock_.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading;

    internal sealed class Configuration : DbMigrationsConfiguration<Stock_.My_DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Stock_.My_DbContext context)
        {
            var costomers = new Dictionary<string, Customer>
            {
                {"Customer1", new Customer {Id = 1, Name = "Customer1", Address="address"}},
                {"Customer2", new Customer {Id = 2, Name = "Customer2", Address="address"}},
                {"Customer3", new Customer {Id = 3, Name = "Customer3", Address = "address"}},
                {"Customer4", new Customer {Id = 4, Name = "Customer4", Address = "address"}},
                {"Customer5", new Customer {Id = 5, Name = "Customer5", Address = "address"}},
                {"Customer6", new Customer {Id = 6, Name = "Customer6", Address = "address"}},
            };

            foreach (var cos in costomers.Values)
                context.Customers.AddOrUpdate(t => t.Id, cos);
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Coca cola", Quantity=5 },
                new Product { Id = 1, Name = "Fanta", Quantity=65 },
                new Product { Id = 1, Name = "Coca cola2", Quantity=2 },
                new Product { Id = 1, Name = "Fanta2", Quantity=13 },
                new Product { Id = 1, Name = "Coca cola3", Quantity=43 }
            };


            foreach (var i in products)
                context.Products.AddOrUpdate(a => a.Id, i);

            var suppliers = new List<Supplier>
            {
                new Supplier { Id = 1, Name = "a1", Address="address" },
                new Supplier { Id = 2, Name = "a2", Address = "address" },
                new Supplier { Id = 3, Name = "a3", Address = "address" },
                new Supplier { Id = 4, Name = "a4", Address = "address" },
                new Supplier { Id = 5, Name = "a5", Address = "address" }
            };
            foreach (var sup in suppliers)
                context.Suppliers.AddOrUpdate(a => a.Id, sup);

        }
     
    }
}
