using Microsoft.Extensions.DependencyInjection;

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
