using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Controler;

namespace WpfApp1.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NameType { get; set; }

        public List<Medicine> Medicines
        {
            get; set;
        }

        public Categories()
        {

        }

        public Categories(string nameType)
        {
            NameType = nameType;
        }

    }
}
