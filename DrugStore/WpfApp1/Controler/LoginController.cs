using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Controler
{
    class LoginController
    {
        public User Login(User user)
        {
            try
            {
                MyDbContext context = new MyDbContext();
                User foundUser = context.Users.FirstOrDefault(User => User.Name == user.Name);
                if (foundUser != null)
                {
                    if (foundUser.Password == user.Password && foundUser.Id != 1)
                    {
                        return foundUser;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public User Admin(User user)
        {
            try
            {
                MyDbContext context = new MyDbContext();
                User foundAdmin = context.Users.FirstOrDefault(User => User.Name == user.Name);
                if (foundAdmin != null)
                {
                    if (foundAdmin.Password == user.Password && foundAdmin.Id == 1)
                    {
                        return foundAdmin;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
