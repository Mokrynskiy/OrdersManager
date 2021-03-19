using OrdersManager.DAL.Entityes;
using System;
using System.Collections.ObjectModel;

namespace OrdersManager.WPF.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthdey { get; set; }
        public Gender Gender { get; set; }
        public DepartmentModel Department { get; set; }
        public ObservableCollection<OrderModel> Orders { get; set; }
    }
}
