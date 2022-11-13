using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreA.Generator
{
    public class FixText
    {
        public static string FixTexts(string text)
        {
            return text.Trim().ToLower();
        }
    }
}
