using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_
{
    public class Sales
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public decimal Price { get; set; }



    }
}
