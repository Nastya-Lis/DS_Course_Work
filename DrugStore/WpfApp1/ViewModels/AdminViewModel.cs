using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;

namespace WpfApp1.ViewModels
{
    public class AdminViewModel:BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new CategoriesViewModel();

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand AdminViewCommand { get; set; }

        public AdminViewModel()
        {
            AdminViewCommand = new AdminViewCommand(this);
        }
    }
}
