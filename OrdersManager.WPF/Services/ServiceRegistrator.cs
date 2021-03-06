using Microsoft.Extensions.DependencyInjection;
using OrdersManager.WPF.Services.Interfaces;

namespace OrdersManager.WPF.Services
{
    static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IEmployeeDialog, EmployeeDialogService>()
            .AddTransient<IOrderDialog, OrderDialogService>()
            .AddTransient<IDepartmentDialog, DepartmentDialogService>()
            ;
    }
}
