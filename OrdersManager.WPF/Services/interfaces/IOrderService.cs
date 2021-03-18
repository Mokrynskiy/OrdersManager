using OrdersManager.DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.Services.interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> Orders { get; }

        Task<Order> CreateOrder(string contractor, DateTime orderDate, Employee employee);
    }
}
