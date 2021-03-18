
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.ViewModels
{
    public class EmployeesViewModel : ViewModel
    {
        private readonly IRepository<Employee> employeeRepository;

        public EmployeesViewModel(IRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
    }
}
