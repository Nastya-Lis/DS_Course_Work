using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using WpfApp1.Controler;

namespace WpfApp1.Models
{
    public class Medicine:INotifyPropertyChanged, ICloneable
    {
        private int ammount;
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
       

        public int CategoriesId{ get; set; }
        public Categories Categories{ get; set;}

        public List<OrderMedicines> OrderMedicine { get; set; }

        public int Ammount
        {   get { return ammount; } 
            set
            {
                if (ammount == value)
                    return;
                else
                {
                    if (value >= 0 )
                    {
                      ammount = value;
                      OnPropertyChanged("Ammount");
                    }
                    
                }
            }
        }

        public Medicine()
        {

        }

        public Medicine(string name,string categoriesName,string providerName,double price,int ammountMed)
        {
            Name = name;
            using(MyDbContext context = new MyDbContext())
            {
                try
                {
                    Categories categoryFromDb = context.Categories.First(category => category.NameType == categoriesName);
                    this.CategoriesId = categoryFromDb.Id;
                }
                catch(Exception)
                {
                     Categories categories = new Categories(categoriesName);                  
                     this.Categories = categories; 
                }
                try
                {
                    Provider providerFromDb = context.Providers.First(provider1 => provider1.Name == providerName);
                    this.ProviderId = providerFromDb.Id;
                }
                catch (Exception)
                {
                    Provider providers = new Provider()
                    {
                        Name = providerName
                    };
                    this.Provider = providers;
                }

            }
            Price = price;
            Ammount = ammountMed;
        }
       

        public object Clone()
        {
            return new Medicine()
            {
                Provider = new Provider() { Id = this.Provider.Id, Medicines = this.Provider.Medicines, 
                    Name = this.Provider.Name, Telephone = this.Provider.Telephone},
                Categories = new Categories() { Id = this.Categories.Id, NameType = this.Categories.NameType },
                Id = this.Id,
                Price = this.Price,
                Name = this.Name,
                CategoriesId = this.CategoriesId,
                Ammount = this.Ammount
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
