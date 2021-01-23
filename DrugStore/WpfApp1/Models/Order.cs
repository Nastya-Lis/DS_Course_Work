using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Order:INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public User User { get; set; }
        public List<OrderMedicines> OrderMedicine { get; set; }

        public Order()
        {

        }

        public Order(User user, DateTime dateTime)
        {
            User = user;
            Date = dateTime;
        }

        public enum State
        {
            Done, Undone
        }

        State state = State.Undone;

        public State States
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("States");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
             
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
