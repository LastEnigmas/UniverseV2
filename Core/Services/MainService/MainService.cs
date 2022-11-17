using CoreA.DTOs.MainDTOs;
using CoreA.Generator;
using CoreA.Security;
using CoreA.Sender;
using Data.Model;
using Data.MyDbCon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CoreA.Generator.ViewToString;

namespace CoreA.Services.MainService
{
    public class MainService : IMainService
    {
        private readonly MyDb _db;
        private readonly IViewRenderService _render;
        public MainService(MyDb db , IViewRenderService render )
        {
            _db = db;
            _render = render;
        }


        public bool IsEmail(string email)
        {
            return _db.Users.Any( u => u.Email == email);
        }
        public bool IsUsername(string username)
        {
            return _db.Users.Any(u => u.Username == username);
        }
        public void Add(User user)
        {
            _db.Users.Add(user);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(User user )
        {
            _db.Update(user);
            Save();
        }
        public User FindUser(string usernameOrEmail)
        {
            return _db.Users.SingleOrDefault(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
        }
        public User FindUserByUsernameOrEmail(SignInViewModel signIn)
        {
            User user  = _db.Users.SingleOrDefault(u => u.Email == signIn.UsernameOrEmail || u.Username == signIn.UsernameOrEmail);
            if(user != null)
            {
                string passwordHelp = HashPasswordC.EncodePasswordMd5(signIn.Password);
                if (user.Password == passwordHelp )
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public RegisterViewModel RegisterUser(string activeCode)
        {
            User user = _db.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if(user != null)
            {
                user.ActiveCode = CreateActiveCode.GenerateCode();
                user.IsActive = true;

                Update(user);

                RegisterViewModel register = new RegisterViewModel()
                {
                    Username = user.Username,
                    Email = user.Email,
                };

                return register;
            }
            else
            {
                return null;
            }
        }
        public bool ForgotPasswordTask(ForgotPasswordViewModel forgot)
        {
            User user = FindUser(forgot.UsernameOrEmail);
            if(user != null)
            {
                string Body = _render.RenderToStringAsync("forgotPassword", user);
                EmailSenders.Send(user.Email , "Register", Body);
                return true;

            }
            else
            {
                return false;
            }
        }
        public User GetUserByActiveCode(string activeCode)
        {
            return _db.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }
    }
}
