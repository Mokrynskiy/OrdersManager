using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public OrdersViewModel OrdersModel => App.Services.GetRequiredService<OrdersViewModel>();
        public EmployeesViewModel EmployeesModel => App.Services.GetRequiredService<EmployeesViewModel>();
        public DepartmentsViewModel DepartmentsModel => App.Services.GetRequiredService<DepartmentsViewModel>();
    }
}
