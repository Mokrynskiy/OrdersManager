using Microsoft.Extensions.DependencyInjection;
using OrdersManager.WPF.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.Services
{
    static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IOrderService, OrderService>()
            ;
    }
}
