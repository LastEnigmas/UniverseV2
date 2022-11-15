using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreA.DTOs.MainDTOs
{
    public class SignOutViewModel
    {
        public string Username { get; set; }
        public bool AreYouSure { get; set; }
        public string StrAreYouSure { get; set; }
    }
}
