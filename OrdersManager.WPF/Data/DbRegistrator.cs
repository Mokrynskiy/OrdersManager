using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersManager.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace OrdersManager.WPF.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<OrdersManagerDbContext>(opt=>
            {
                var type = configuration["Type"];
                switch (type)
                {
                    case null:
                        throw new InvalidOperationException("Не определен тип БД");
                    default:
                        throw new InvalidOperationException($"Тип БД {type} не поддерживается");
                    case "MSSQL":
                        opt.UseSqlServer(configuration.GetConnectionString(type));
                        break;
                    case "SQLite":
                        opt.UseSqlite(configuration.GetConnectionString(type));
                        break;
                    
                }
            })
            ;
    }
}
