using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;

namespace WpfApp1.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
		private BaseViewModel selectedViewModel = new LoginViewModel();

		public MainViewModel()
		{
			UpdateViewCommand = new UpdateViewCommand(this);
		}
		public BaseViewModel SelectedViewModel
		{
			get { return selectedViewModel; }
			set {
				selectedViewModel = value;
				OnPropertyChanged(nameof(SelectedViewModel));		
			}
		}

		public ICommand UpdateViewCommand { get; set; }

	}
}
