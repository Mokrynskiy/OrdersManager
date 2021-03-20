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
        
        public DepartmentModel Department { get; set; }     
        public string Title { get; set; }

        public DepartmentEditViewModel(DepartmentModel departmentModel, string title)
        {
            Department = departmentModel;            
            Title = title;
        }
    }
}
