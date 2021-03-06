using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using OrdersManager.WPF.Services.Interfaces;
using OrdersManager.WPF.ViewModels;
using OrdersManager.WPF.Views.Windows;
using System.Collections.Generic;

namespace OrdersManager.WPF.Services
{
    public class EmployeeDialogService:IEmployeeDialog
    {       
        public bool Edit(EmployeeModel employeModel, IEnumerable<Department> departments, string title, bool enableSelectDepartment)
        {
            var editingEmployeeVm = new EmployeeEditViewModel(employeModel, departments, title ,enableSelectDepartment);
            var editEmployeeWindow = new EmployeeEditWindow
            {
                DataContext = editingEmployeeVm
            };
            if (editEmployeeWindow.ShowDialog() != true) return false;

            return true;
        }
    }
}
