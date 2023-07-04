using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_
{
    public class Customer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public  ICollection<Sales> Sales { get; set; }
    }
}
