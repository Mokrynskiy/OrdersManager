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

namespace OrdersManager.WPF.Services
{
    public class DepartmentDialogService : IDepartmentDialog
    {
        public bool Edit(DepartmentModel departmentModel, IEnumerable<Employee> employees, string title)
        {
            var editingDepartmentVm = new DepartmentEditViewModel(departmentModel, employees, title);
            var editEmployeeWindow = new DepartmentEditWindow
            {
                DataContext = editingDepartmentVm
            };
            if (editEmployeeWindow.ShowDialog() != true) return false;

            return true;
        }
    }
}
