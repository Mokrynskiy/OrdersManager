using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using System.Collections.Generic;

namespace OrdersManager.WPF.Services.Interfaces
{
    public interface IDepartmentDialog
    {
        public bool Edit(DepartmentModel departmentModel,  string title);
    }
}
