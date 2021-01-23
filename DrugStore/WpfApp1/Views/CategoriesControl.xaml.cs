using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Controler;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для CategoriesControl.xaml
    /// </summary>
    public partial class CategoriesControl : UserControl
    {
       
        MyDbContext context = new MyDbContext();
        public CategoriesControl()
        {
            InitializeComponent();
            context.Categories.Load();
            BindingList<Categories> categories = context.Categories.Local.ToBindingList();
            CategoriesDataGrid.ItemsSource = categories;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyDbContext context = new MyDbContext();
                CategoriesController categoriesController = new CategoriesController();
                Categories category= new Categories(txtCategoriesType.Text);
                if (int.TryParse(txtCategoriesType.Text, out int categoryint) == false)
                {

                    if (categoriesController.AddCategory(category))
                    {
                        MessageBox.Show("Категория успешно добавлена.");
                        CategoriesDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                        List<Categories> categories = context.Categories.ToList();
                        CategoriesDataGrid.ItemsSource = categories;
                    }
                    else
                    {
                        MessageBox.Show("Эта категория уже существует или введенные данные имеют неверный формат.");
                    }
                }
                else
                    MessageBox.Show("Неверный формат данных");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyDbContext context = new MyDbContext();
                CategoriesController categoriesController = new CategoriesController();
                Categories category = CategoriesDataGrid.SelectedItem as Categories;
                if (CategoriesDataGrid.SelectedItem != null)
                {
                    context.Categories.Attach(category);
                    categoriesController.RemoveCategory(category);
                    MessageBox.Show("Категория успешно удалена.");
                }
                CategoriesDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                List<Categories> categories = context.Categories.ToList();
                CategoriesDataGrid.ItemsSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                context.SaveChanges();
                CategoriesDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                List<Categories> categories = context.Categories.ToList();
                CategoriesDataGrid.ItemsSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

