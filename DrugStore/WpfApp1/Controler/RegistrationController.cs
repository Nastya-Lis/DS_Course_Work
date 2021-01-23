using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Controler
{
    class RegistrationController
    {
        public bool CheckIfUserExists(User user)
        {
                MyDbContext context = new MyDbContext();
                
                if (context.Users.FirstOrDefault(User => User.Name == user.Name) != null)
                {
                    return true;
                }else if (context.Users.FirstOrDefault(User => User.Email == user.Email) != null)
                {
                    return true;
                }
                return false;
        }
        public bool Register(User user)
        {
            try
            {
                if (!CheckIfUserExists(user))
                {
                    MyDbContext context = new MyDbContext();
                    context.Users.Add(user);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
