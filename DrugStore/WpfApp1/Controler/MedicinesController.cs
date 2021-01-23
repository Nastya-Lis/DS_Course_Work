using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Controler
{
    class MedicinesController
    {

        public bool CheckIfMedicineExists(Medicine medicine)
        {
            MyDbContext context = new MyDbContext();
            if (context.Medicines.FirstOrDefault(myMedicine => myMedicine.Id == medicine.Id) != null
                    && context.Medicines.FirstOrDefault(myMedicine2 => myMedicine2.ProviderId == medicine.ProviderId) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddMedicine(Medicine medicine)
        {
            try
            {
                if (!CheckIfMedicineExists(medicine))
                {
                    MyDbContext context = new MyDbContext();
                    context.Medicines.Add(medicine);
                    context.SaveChanges();
                    return true;

                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool RemoveMedicine(Medicine medicine)
        {
            try
            {
                if (CheckIfMedicineExists(medicine))
                {
                    MyDbContext context = new MyDbContext();
                    context.Medicines.Attach(medicine);
                    context.Medicines.Remove(medicine);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
