using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.ViewModels
{
    static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModel(this IServiceCollection services) => services
            .AddTransient<MainWindowViewModel>()
            .AddTransient<OrdersViewModel>()
            .AddTransient<EmployeesViewModel>()
            .AddTransient<DepartmentsViewModel>()
        ;
    }
}
