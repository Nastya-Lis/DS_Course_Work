using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.ViewModels;

namespace WpfApp1.Commands
{
    class AdminViewCommand:ICommand
    {
        private AdminViewModel viewModel1;

        public AdminViewCommand(AdminViewModel viewModel1)
        {
            this.viewModel1 = viewModel1;
        }

       public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Categories")
            {
                viewModel1.SelectedViewModel = new CategoriesViewModel();
            }

            else if (parameter.ToString() == "Medicines")
            {
                viewModel1.SelectedViewModel = new MedicineViewModel();
            }

            else if (parameter.ToString() == "Orders")
            {
                viewModel1.SelectedViewModel = new OrdersViewModel();
            }
            else if (parameter.ToString() == "Providers")
            {
                viewModel1.SelectedViewModel = new ProvidersViewModel();
            }
        }  
       
    }
}
