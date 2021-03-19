using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.ViewModels
{
    public class EmployeeEditViewModel : ViewModel
    {
        public EmployeeModel Employee { get; set; }
        
        public string Name { get; set; }
        public EmployeeEditViewModel(EmployeeModel employee)
        {
            Employee = employee;
            Name = employee.Name;
        }

        
    }
}
