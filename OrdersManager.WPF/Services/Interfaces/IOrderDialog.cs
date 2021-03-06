using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using System.Collections.Generic;

namespace OrdersManager.WPF.Services.Interfaces
{
    public interface IOrderDialog
    {
        public bool Edit(OrderModel orderModel, IEnumerable<Employee> employees, string title, bool enableSelectEmployee);
    }
}
