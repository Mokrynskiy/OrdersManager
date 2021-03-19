using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using OrdersManager.WPF.Services.Interfaces;
using OrdersManager.WPF.ViewModels;
using OrdersManager.WPF.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrdersManager.WPF.Services
{
    public class EmployeeDialogService:IEmployeeDialog
    {
        public bool Edit(EmployeeModel employeeModel)
        {
            var editingEmployeeVm = new EmployeeEditViewModel(employeeModel);
            var editEmployeeWindow = new EmployeeEditWindow
            {
                DataContext = employeeModel
            };
            if (editEmployeeWindow.ShowDialog() != true) return false;
            MessageBox.Show(editingEmployeeVm.Name
                );
            return true;
        }
                
    }
}
