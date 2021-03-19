using Microsoft.Extensions.DependencyInjection;
using OrdersManager.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.Services
{
    static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IEmployeeDialog, EmployeeDialogService>()
            .AddTransient<IOrderDialog, OrderDialogService>()
            ;
    }
}
