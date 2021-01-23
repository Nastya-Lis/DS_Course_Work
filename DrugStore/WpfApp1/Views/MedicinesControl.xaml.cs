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
    /// Логика взаимодействия для MedicinesControl.xaml
    /// </summary>
    public partial class MedicinesControl : UserControl
    {
       MyDbContext context = new MyDbContext();
      
        public MedicinesControl()
        {
            InitializeComponent();
            context.Medicines.Load();
            List<Medicine> medicines = context.Medicines.Include(med => med.Categories).Include(med1 => med1.Provider).ToList();
            MedicineDataGrid.ItemsSource = medicines;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyDbContext context = new MyDbContext();
                MedicinesController medicinesController = new MedicinesController();
                if (double.TryParse(PriceMedicine.Text, out double price) == true && int.TryParse(AmmountMedicine.Text, out int ammount) == true
                     && int.TryParse(NameMedicine.Text, out int medicineint) == false
                      && int.TryParse(CategoriesMedicine.Text, out int categoryint) == false
                       && int.TryParse(ProviderMedicine.Text, out int providerint) == false
                    )
                {
                    double priceMed = double.Parse(PriceMedicine.Text);
                    int ammountMed = int.Parse(AmmountMedicine.Text);
                    Medicine medicine = new Medicine(NameMedicine.Text, CategoriesMedicine.Text, ProviderMedicine.Text, priceMed, ammountMed);

                    if (medicinesController.AddMedicine(medicine))
                    {
                        MessageBox.Show("Препарат успешно добавлен.");
                        MedicineDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                        List<Medicine> medicines = context.Medicines.Include(med => med.Provider).Include(med1 => med1.Categories).ToList();
                        MedicineDataGrid.ItemsSource = medicines;
                    }
                    else
                    {
                        MessageBox.Show("Этот препарат уже существует или введенные данные имеют неверный формат.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат данных");
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
                MedicinesController medicinesController = new MedicinesController();
                Medicine medicine = MedicineDataGrid.SelectedItem as Medicine;
                if (MedicineDataGrid.SelectedItem != null)
                {
                    context.Medicines.Attach(medicine);
                    medicinesController.RemoveMedicine(medicine);
                    MessageBox.Show("Препарат успешно удален.");
                }
                MedicineDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                List<Medicine> medicines = context.Medicines.Include(med => med.Provider).Include(med1 => med1.Categories).ToList();
                MedicineDataGrid.ItemsSource = medicines;
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
                MedicineDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                List<Medicine> medicines = context.Medicines.Include(med => med.Provider).Include(med1 => med1.Categories).ToList();
                MedicineDataGrid.ItemsSource = medicines;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
