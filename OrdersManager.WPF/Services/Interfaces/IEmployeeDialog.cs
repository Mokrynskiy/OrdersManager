using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using System.Collections.Generic;

namespace OrdersManager.WPF.Services.Interfaces
{
    public interface IEmployeeDialog
    {
        public bool Edit(EmployeeModel employeModel, IEnumerable<Department> departments, string title, bool enableSelectDepartment);
    }
}
