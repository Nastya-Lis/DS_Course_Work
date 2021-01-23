using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Controler;

namespace WpfApp1.Models
{
    [Table("Users")]
    public class User: INotifyPropertyChanged
    {
        private List<Order> listOrders = new List<Order>();
        List<Order> reserv = new List<Order>();
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(16)]
        public string Name { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        private void SetPassword(string password)
        {
            Password = ComputeSha256Hash(password);
        }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

  
        public IList<Order> Orders{ get; set;}
        public User()
        {
            Orders = new List<Order>();
        }

        public User(string name, string password, string email)
        { 
            SetPassword(password);
            Name = name;
            Email = email;
            Orders = new List<Order>();
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
