using OrdersManager.DAL.Entityes;
using System;
using System.Collections.ObjectModel;

namespace OrdersManager.WPF.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePatronymic { get; set; }
        public DateTime EmployeeBirthdey { get; set; }
        public Gender EmployeeGender { get; set; }
        public DepartmentModel Department { get; set; }
        public ObservableCollection<OrderModel> Orders { get; set; }
    }
}
