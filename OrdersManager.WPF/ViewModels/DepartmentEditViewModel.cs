using MathCore.WPF.ViewModels;
using OrdersManager.WPF.Models;

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
