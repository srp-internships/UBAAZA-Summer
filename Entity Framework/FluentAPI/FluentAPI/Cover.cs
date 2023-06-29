using DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAPI
{
    public class Cover
    {
        public int ID { get; set; }
        public Course Course { get; set; }
    }
}
