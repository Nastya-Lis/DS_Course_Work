using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Controler;

namespace WpfApp1.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Telephone { get; set; }
        public List<Medicine> Medicines{ get; set;}

        public Provider()
        {

        }

        public Provider(string name, string telephone)
        {
            Name = name;
            Telephone = telephone;
        }

    }
}
