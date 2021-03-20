using OrdersManager.WPF.Models;

namespace OrdersManager.WPF.Services.Interfaces
{
    public interface IDepartmentDialog
    {
        public bool Edit(DepartmentModel departmentModel,  string title);
    }
}
