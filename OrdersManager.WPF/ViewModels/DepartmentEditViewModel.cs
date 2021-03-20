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
    public class DepartmentEditViewModel : ViewModel
    {
        private DepartmentModel _departmentModel;
        private IEnumerable<Employee> _employees;
        public string Title { get; set; }

        public DepartmentEditViewModel(DepartmentModel departmentModel, IEnumerable<Employee> employees, string title)
        {
            _departmentModel = departmentModel;
            _employees = employees;
            Title = title;
        }
    }
}
