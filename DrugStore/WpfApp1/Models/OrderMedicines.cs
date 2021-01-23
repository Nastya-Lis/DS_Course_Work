using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApp1.Controler;

namespace WpfApp1.Models
{
    public class OrderMedicines
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        public int AmmountMedicine { get; set; }

        public OrderMedicines()
        {

        }
        public OrderMedicines(int orderId, int medicineId,int ammount)
        {
            OrderId = orderId;
            MedicineId = medicineId;
            AmmountMedicine = ammount;
        }
    }
}
