using CoreA.DTOs.MainDTOs;
using CoreA.Generator;
using CoreA.Security;
using Data.Model;
using Data.MyDbCon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreA.Services.MainService
{
    public class MainService : IMainService
    {
        private readonly MyDb _db;
        public MainService(MyDb db)
        {
            _db = db;
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
    }
}
