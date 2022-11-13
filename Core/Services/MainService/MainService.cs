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
    }
}
