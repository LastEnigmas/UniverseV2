using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreA.Services.MainService
{
    public interface IMainService
    {
        bool IsEmail(string email );
        bool IsUsername(string username );
        void Add(User user);
        void Update(User user);
        void Save();

    }
}
