using Microsoft.Extensions.DependencyInjection;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;

namespace OrdersManager.DAL.Repositoryes
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoryesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Department>, DepartmentsReposytory>()
            .AddTransient<IRepository<Employee>, EmploeesRepository>()
            .AddTransient<IRepository<Order>, OrdersRepository>()
            ;
    }
}
