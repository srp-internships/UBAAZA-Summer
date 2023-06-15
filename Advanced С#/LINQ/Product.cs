using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public class Product
    {
        public int Count { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }

       public static List<Product> GetCustomerList()
        {
            List<Product> products = new List<Product>()
            {
                new Product(){Count=10,Id=Guid.NewGuid(),Name="Apple" },
                new Product(){Count=20,Id=Guid.NewGuid(),Name="Banana" },
                new Product(){Count=40,Id=Guid.NewGuid(),Name="Grape" },
                new Product(){Count=20,Id=Guid.NewGuid(),Name="Cherry" }
            };
            return products;
        }

       
    }
}
