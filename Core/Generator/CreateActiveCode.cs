using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreA.Generator
{
    public class CreateActiveCode
    {
        public static string GenerateCode()
        {
            return Guid.NewGuid().ToString().Replace('-', '2');
        }
    }
}
