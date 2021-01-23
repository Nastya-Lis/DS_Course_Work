using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Логика взаимодействия для OrdersControl.xaml
    /// </summary>
    public partial class OrdersControl : UserControl
    {
        string adminMail;
        string sendMail;
        DataGridRow selectedRow;
        MyDbContext context = new MyDbContext();

        public OrdersControl()
        {
            InitializeComponent();
            context.Orders.Load();
            adminMail = context.Users.First(admin => admin.Id == 1).Email;
            List<Order> orders = context.Orders.Include(r => r.User).ToList();
            OrderDataGrid.ItemsSource = orders;
        }

        private void DataGrid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
                DataGridRow row = sender as DataGridRow;
                TextBlock tbl = OrderDataGrid.Columns[2].GetCellContent(row) as TextBlock;
                sendMail = tbl.Text;
                selectedRow = row;
        }

        private void ChangeOrderState()
        {
            if (selectedRow != null)
            {
                TextBlock idOrderBlock = OrderDataGrid.Columns[1].GetCellContent(selectedRow) as TextBlock;
                int idOrder = Int32.Parse(idOrderBlock.Text);
                using (MyDbContext context = new MyDbContext())
                {
                    try
                    {
                        Order orderChangeState = context.Orders.First(order => order.Id == idOrder);
                        orderChangeState.States = Order.State.Done;
                        OrderDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                        List<Order> orders = context.Orders.Include(r => r.User).ToList();
                        OrderDataGrid.ItemsSource = orders;
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}");
                    }
                }
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRow != null)
            {
                TextBlock stateOrderBlock = OrderDataGrid.Columns[4].GetCellContent(selectedRow) as TextBlock;
                if (stateOrderBlock.Text != "Done")
                {
                    if (sendMail != null)
                    {
                        try
                        {
                        MailAddress mailAddressFrom = new MailAddress(adminMail, "Твоя Аптечка");
                        
                        MailAddress mailAddressTo = new MailAddress(sendMail);

                        MailMessage message = new MailMessage(mailAddressFrom, mailAddressTo);

                        message.Subject = "Оповещение о выполнении заказа";
                        message.Body = "Ваш заказ выполнен \n Благодарим за то, что воспользовались нашими услугами!";
                        SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(adminMail, "primitekursach"); 
                        smtp.Send(message);
                        MessageBox.Show("Успешное отправление");
                        ChangeOrderState();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }
                        finally
                        {
                            selectedRow = null;
                        }
                    }
                    else
                        MessageBox.Show("Что-то пошло не так");
                }
                else
                {
                    MessageBox.Show("Заказ уже выполнен");
                    selectedRow = null;
                }
            }
            else
            {
                MessageBox.Show("Попробуйте дважды нажать на нужный заказ");
                selectedRow = null;
            }
              
        }
    }
}
