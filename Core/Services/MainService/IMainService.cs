using CoreA.DTOs.MainDTOs;
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
        User FindUserByUsernameOrEmail(SignInViewModel signIn );
        bool IsEmail(string email );
        bool IsUsername(string username );
        User FindUser(string usernameOrEmail);
        User GetUserByActiveCode(string activeCode);
        RegisterViewModel RegisterUser(string activeCode);
        bool ForgotPasswordTask(ForgotPasswordViewModel forgot);
        void Add(User user);
        void Update(User user);
        void Save();

    }
}
