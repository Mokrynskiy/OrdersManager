using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.ViewModels
{
    public class DepartmentsViewModel : ViewModel
    {
        private IRepository<Department> departmentsRepository;

        public DepartmentsViewModel(IRepository<Department> departmentsRepository)
        {
            this.departmentsRepository = departmentsRepository;
        }
    }
}
