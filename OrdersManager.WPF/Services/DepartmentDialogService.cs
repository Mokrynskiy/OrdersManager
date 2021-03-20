using OrdersManager.WPF.Models;
using OrdersManager.WPF.Services.Interfaces;
using OrdersManager.WPF.ViewModels;
using OrdersManager.WPF.Views.Windows;

namespace OrdersManager.WPF.Services
{
    public class DepartmentDialogService : IDepartmentDialog
    {
        public bool Edit(DepartmentModel departmentModel, string title)
        {
            var editingDepartmentVm = new DepartmentEditViewModel(departmentModel,  title);
            var editEmployeeWindow = new DepartmentEditWindow
            {
                DataContext = editingDepartmentVm
            };
            if (editEmployeeWindow.ShowDialog() != true) return false;

            return true;
        }
    }
}
