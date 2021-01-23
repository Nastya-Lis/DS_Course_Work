using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Controler
{
    class MyDbContext:DbContext
    {
        public MyDbContext():base("drugStroreConnection")
        {

        }

        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(new MyDbInitialize());
        }

        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Medicine> Medicines { get; set; }
        public DbSet<Models.OrderMedicines> OrderMedicines { get; set; }
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<Models.Provider> Providers { get; set; }
        public DbSet<Models.Categories> Categories { get; set; }      
    }
}
