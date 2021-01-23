using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WpfApp1.Controler;
using WpfApp1.Models;
using System.Data.Entity;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.CodeDom;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using WpfApp1.Views;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
        User currentUser;
        public List<Medicine> medicineListToBasket = new List<Medicine>();
        List<Medicine> medicines = new List<Medicine>();
        double counter;
        public double Counter
        {
            get { return counter; }
            set
            {
                counter = value;
            }
        }

        public List<Medicine> MedicinesToMain
        {
            get { return medicines; }
        }

      
        public MainWindow(User user)
        {
            currentUser = user;
            InitializeComponent();
            Title = user.Name;
        } 
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SearchComboBox.SelectedItem != null && SearchText.Text != "")
            {
                switch (((TextBlock)SearchComboBox.SelectedItem).Text.ToString())
                {
                    case "Название":
                        MyDbContext context = new MyDbContext();
                        List<Medicine> fromDb = context.Medicines.Include(u => u.Categories).Include(p => p.Provider).ToList();
                        List<Medicine> ended = new List<Medicine>();
                        string str = SearchText.Text;
                        var toD2 = from f in fromDb where f.Name.Contains($"{str}") select f;
                        foreach (var item in toD2)
                        {
                            ended.Add(item);
                        }
                        if (ended.Count!=0)
                            MedicineDataGrid.ItemsSource = ended;
                        else
                            MessageBox.Show("Не найдено совпадений");
                        break;
                    case "Категория":
                        MyDbContext context1 = new MyDbContext();
                        List<Medicine> fromDb1 = context1.Medicines.Include(p => p.Provider).Include(u => u.Categories).ToList();
                        List<Medicine> ended1 = new List<Medicine>();
                        string str1 = SearchText.Text;
                        var toD1 = from f in fromDb1 where f.Categories.NameType.Contains($"{str1}") select f;
                        foreach (var item in toD1)
                        {
                            ended1.Add(item);
                        }
                        if (ended1.Count != 0)
                            MedicineDataGrid.ItemsSource = ended1;
                        else
                            MessageBox.Show("Не найдено совпадений");
                        break;
                    case "Производитель":
                        MyDbContext context3 = new MyDbContext();
                        List<Medicine> fromDb2 = context3.Medicines.Include(p => p.Provider).Include(u => u.Categories).ToList();
                        List<Medicine> ended3 = new List<Medicine>();
                        string str3 = SearchText.Text;
                        var toD3 = from f in fromDb2 where f.Provider.Name.Contains($"{str3}") select f;
                        foreach (var item in toD3)
                        {
                            ended3.Add(item);
                        }
                        if (ended3.Count != 0)
                            MedicineDataGrid.ItemsSource = ended3;
                        else
                            MessageBox.Show("Не найдено совпадений");

                        break;
                }
            }
            else
            {
                MessageBox.Show("Задайте фильтр поиска!!!!!!!");
            }
        }

        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                using (MyDbContext context = new MyDbContext())
                {
                    Medicine medicine = MedicineDataGrid.SelectedItem as Medicine;

                    Medicine medicineCopy = (Medicine)medicine.Clone();
                    if (medicine.Ammount != 0)
                    {
                        try
                        {
                            medicine.Ammount = medicine.Ammount - 1;
                            medicineCopy.Ammount = 1;
                            MedicineDataGridBasket.Items.Add(medicineCopy);
                            medicines.Add(medicine);
                            counter += medicine.Price;
                            SummaryCost.Text = counter.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Препарат закончился");
                    }
                }
            }
        }

        private void ForBasketDel()
        {
            using (MyDbContext context = new MyDbContext())
            {
                try
                {
                    Medicine medicine = MedicineDataGridBasket.SelectedItem as Medicine;
                    var fromDbMedicine = context.Medicines.Single(x => x.Id == medicine.Id);
                    foreach (var item in medicines)
                    {
                        if (item.Id == fromDbMedicine.Id)
                        {
                            medicines.Remove(item);
                            break;
                        }
                    }

                    var changeAmmount = (List<Medicine>)MedicineDataGrid.ItemsSource;
                    foreach (var item in changeAmmount)
                    {
                        if (fromDbMedicine.Id == item.Id)
                        {
                            item.Ammount = item.Ammount + 1;
                        }
                    }
                    medicineListToBasket.Remove(medicine);
                    MedicineDataGridBasket.Items.Remove(MedicineDataGridBasket.SelectedItem);
                    if (medicines.Count != 0)
                    {
                        counter -= medicine.Price;
                        SummaryCost.Text = counter.ToString();
                    }
                    else
                    {
                        counter = 0;
                        SummaryCost.Text = counter.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }

            }
        }

        private void returnDataRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                ForBasketDel();
            }
        } 
        
        private void Order_Click(object sender, RoutedEventArgs e)
        {
            var forOrderList = medicines;
            if (forOrderList.Count != 0)
            {
                OrderFormWindow orderForm = new OrderFormWindow(forOrderList);
                orderForm.Show();
                Check();
            }
            else
                MessageBox.Show("Корзина пуста");
        }

        private bool Check()
        {
            using (MyDbContext context = new MyDbContext())
            {
                foreach (Medicine medicine in medicines)
                {
                    Medicine medicineInDb = context.Medicines.Find(medicine.Id);
                    if (medicineInDb != null)
                    {
                        medicineInDb.Ammount--;
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("ЫыыЫ");

                    }
                }
            }

            return false;
        }
  
        private void ClearBasketFunction()
        {
            if (medicines.Count != 0)
            {
                using (MyDbContext context = new MyDbContext())
                {
                    int count = MedicineDataGridBasket.Items.Count;
                    int count1 = count; 
                    for ( int i = 0; i < count1; i++) 
                    {
                        var it = MedicineDataGridBasket.Items[i] as Medicine;
                        var fromDbMedicine = context.Medicines.Single(x => x.Id == it.Id);
                        var changeAmmount = (List<Medicine>)MedicineDataGrid.ItemsSource;
                        foreach (var item in changeAmmount)
                        {
                            if (fromDbMedicine.Id == item.Id)
                            {
                                item.Ammount = item.Ammount + 1; 
                            }
                        }
                        MedicineDataGridBasket.Items.Remove(MedicineDataGridBasket.Items[i]);
                        count1--;
                        i--;
                    }
                }
                MedicineDataGridBasket.Items.Clear();
                MedicineDataGridBasket.Items.Refresh();
                medicines.Clear();
                counter = 0;
                SummaryCost.Text = counter.ToString();
            }
        }

        private void ClearBasket_Click(object sender, RoutedEventArgs e)
        {
            if (medicines.Count != 0)
            {
                MessageBoxResult boxResult = MessageBox.Show("Уверены,что хотите очистить корзину?", "Очистка корзины",
                      MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                switch (boxResult)
                {
                    case MessageBoxResult.Yes:
                        ClearBasketFunction();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else
                MessageBox.Show("Корзина и так пуста");
        }     
    }
}
