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
    /// Логика взаимодействия для ProvidersControl.xaml
    /// </summary>
    public partial class ProvidersControl : UserControl
    {
        MyDbContext context = new MyDbContext();
        public ProvidersControl()
        {
            InitializeComponent();
            context.Providers.Load();
            BindingList<Provider> providers = context.Providers.Local.ToBindingList();
            ProviderDataGrid.ItemsSource = providers;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyDbContext context = new MyDbContext();
                ProvidersController providersController = new ProvidersController();
                Provider provider = new Provider(NameProvider.Text, Telephone.Text);

                if (providersController.AddProvider(provider))
                {
                    MessageBox.Show("Поставщик успешно добавлен.");
                    ProviderDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                    List<Provider> providers = context.Providers.ToList();
                    ProviderDataGrid.ItemsSource = providers;
                }
                else
                {
                    MessageBox.Show("Этот поставщик уже был добавлен или введенные данные имеют неверный формат.");
                }
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
                ProvidersController providersController = new ProvidersController();
                Provider provider = ProviderDataGrid.SelectedItem as Provider;
                if (ProviderDataGrid.SelectedItem != null)
                {
                    context.Providers.Attach(provider);
                    providersController.RemoveProvider(provider);
                    MessageBox.Show("Поставшик успешно удален.");
                }
                ProviderDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                List<Provider> providers = context.Providers.ToList();
                ProviderDataGrid.ItemsSource = providers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Telephone.Text.Length < 13)
            {
                try
                {
                    context.SaveChanges();
                    ProviderDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                    List<Provider> providers = context.Providers.ToList();
                    ProviderDataGrid.ItemsSource = providers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Превышен диапазон");
        }
    }
}
