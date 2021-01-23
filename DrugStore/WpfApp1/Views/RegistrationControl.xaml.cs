using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WpfApp1.Commands;
using WpfApp1.Controler;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationControl.xaml
    /// </summary>
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl()
        {
            InitializeComponent();
          
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationController controller = new RegistrationController();
            if (Regex.Match(Password.Password, @"^[0-9]+$").Success &&
                Regex.Match(UserName.Text, @"^[а-яА-ЯёЁa-zA-Z]+[а-яА-ЯёЁa-zA-Z0-9]+$").Success && UserName.Text.Length > 4)
            {
                if (Password.Password.Length >= 6 && Password.Password == PasswordRepit.Password)
                {
                    User user = new User(UserName.Text, Password.Password, Email.Text);
                    controller.Register(user);
                    if (new LoginControl().login(user))
                    {
                        LoginWindow parentWindow = Window.GetWindow(this) as LoginWindow;
                        parentWindow.Close();
                    }
                }
                else
                    MessageBox.Show("Пароли не совпадают или слишком короткий пароль");
            }
            else
                MessageBox.Show("Введены некорректные символы");
        }
    }
}
