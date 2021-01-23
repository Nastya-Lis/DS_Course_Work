using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WpfApp1.ViewModels;

namespace WpfApp1.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand( MainViewModel _viewModel)
        {
            viewModel = _viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.ToString() == "Login")
            {
                viewModel.SelectedViewModel = new LoginViewModel();
            }
            else if (parameter.ToString() == "Registration")
            {
                viewModel.SelectedViewModel = new RegistrationViewModel(); 
            }
        }
    }
}
