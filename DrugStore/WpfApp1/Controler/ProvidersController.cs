using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Controler
{
    class ProvidersController
    {
        public bool CheckIfProvidersExists(Provider provider)
        {
            MyDbContext context = new MyDbContext();
            if (context.Providers.FirstOrDefault(Provider => Provider.Name == provider.Name) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddProvider(Provider provider)
        {
            try
            {
                if (!CheckIfProvidersExists(provider))
                {
                   
                        MyDbContext context = new MyDbContext();
                        context.Providers.Add(provider);
                        context.SaveChanges();
                        return true;
                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool RemoveProvider(Provider provider)
        {
            try
            {
                if (CheckIfProvidersExists(provider))
                {
                    MyDbContext context = new MyDbContext();
                    context.Providers.Attach(provider);
                    context.Providers.Remove(provider);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
