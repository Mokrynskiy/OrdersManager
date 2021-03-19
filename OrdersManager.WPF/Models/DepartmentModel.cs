using System.Collections.ObjectModel;

namespace OrdersManager.WPF.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerId { get; set; }
        public EmployeeModel Manager { get; set; }
        public ObservableCollection<EmployeeModel> Employees { get; set; }
    }
}
