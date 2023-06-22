using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{ 
   public class FileReader: IFileReader
    {
        public string Read(string path) 
        {
         return File.ReadAllText(path);
        }
    }
}
