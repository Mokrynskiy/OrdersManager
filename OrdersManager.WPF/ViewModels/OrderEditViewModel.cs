using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using System.Collections.Generic;

namespace OrdersManager.WPF.ViewModels
{
    public class OrderEditViewModel : ViewModel
    {
        private OrderModel _order;
        public OrderModel Order { get => _order; set => Set(ref _order, value); }
        public string Title { get; set; }
        public IEnumerable<Employee> Employees { get; private set; }        
        public bool EnableSelectEmployee { get; private set; }

        public OrderEditViewModel(OrderModel orderModel, IEnumerable<Employee> employees, string title, bool enableSelectEmployee)
        {
            _order = orderModel;
            Employees = employees;
            Title = title;
            EnableSelectEmployee = enableSelectEmployee;
        }
    }
}
