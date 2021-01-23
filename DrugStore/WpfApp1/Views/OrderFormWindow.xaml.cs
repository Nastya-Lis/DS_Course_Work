using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using System.Security.Cryptography;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderFormWindow.xaml
    /// </summary>
    public partial class OrderFormWindow : Window
    {
        public List<Medicine> medicineToOrder = new List<Medicine>(); 

        public OrderFormWindow(List<Medicine> _medicine)
        {
            InitializeComponent();
            medicineToOrder = _medicine;
        }


        private void ClearDataGridFromMain()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MedicinesToMain.Clear();
                    (window as MainWindow).MedicineDataGridBasket.Items.Clear();
                    (window as MainWindow).MedicineDataGridBasket.Items.Refresh();
                    (window as MainWindow).Counter = 0;
                    (window as MainWindow).SummaryCost.Text = (window as MainWindow).Counter.ToString();
                }
            }
        }


        static public string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData)); 
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
                using (MyDbContext context = new MyDbContext())
                {
                    User userAgain = context.Users.FirstOrDefault(User => User.Email == EmailAgain.Text);
                    if (userAgain != null)
                    {
                      if (userAgain.Password == ComputeSha256Hash(PasswordAgain.Password))
                      {
                        List<OrderMedicines> orderMedicines = new List<OrderMedicines>();
                        Order order = new Order();
                        for (int i = 0; i < medicineToOrder.Count; i++)
                        {
                            orderMedicines.Add(new OrderMedicines(order.Id, medicineToOrder[i].Id, 1));
                        }
                        order.Date = DateTime.Now;
                        order.OrderMedicine = orderMedicines;
                        MessageBox.Show($"Заказ оформлен");
                        userAgain.Orders.Add(order);
                        context.SaveChanges();
                        ClearDataGridFromMain();
                        OrderFormWindow parentWindow = Window.GetWindow(this) as OrderFormWindow;
                        parentWindow.Close();
                      }
                       else
                        MessageBox.Show("Несовпадение пароля или почты, числящихся в записи пользователя");
                    }
                    else
                    MessageBox.Show("Несовпадение пароля или почты, числящихся в записи пользователя");
            }
        }
    }
}
