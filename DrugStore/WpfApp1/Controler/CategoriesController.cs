using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Controler
{
    class CategoriesController
    {
        public bool CheckIfCategoriesExists(Categories category)
        {
            MyDbContext context = new MyDbContext();
            if (context.Categories.FirstOrDefault(Category => Category.NameType == category.NameType) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddCategory(Categories category)
        {
            try
            {
                if (!CheckIfCategoriesExists(category))
                {
                    MyDbContext context = new MyDbContext();
                    context.Categories.Add(category);
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

        public bool RemoveCategory(Categories category)
        {
            try
            {
                if (CheckIfCategoriesExists(category))
                {
                    MyDbContext context = new MyDbContext();
                    context.Categories.Attach(category);
                    context.Categories.Remove(category);
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
