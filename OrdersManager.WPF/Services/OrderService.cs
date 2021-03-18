using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.Services
{
    public class OrderService:IOrderService
    {
        private readonly IRepository<Employee> _employees;
        private readonly IRepository<Order> _orders;
        public IEnumerable<Order> Orders => _orders.Items;
        public OrderService(IRepository<Employee> employees, IRepository<Order> orders)
        {
            _employees = employees;
            _orders = orders;
        }
        public async Task<Order> CreateOrder(string contractor, DateTime orderDate, Employee employee)
        {
            var Order = new Order
            {
                Contractor = contractor,
                Date = orderDate,
                Author = employee
            };
            return await _orders.AddAsync(Order);
        }
    }
}
