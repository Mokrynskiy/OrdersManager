using Microsoft.Extensions.DependencyInjection;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.DAL.Repositoryes
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoryesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Department>, DbRepository<Department>>()
            .AddTransient<IRepository<Employee>, DbRepository<Employee>>()
            .AddTransient<IRepository<Order>, DbRepository<Order>>()
            ;
    }
}
