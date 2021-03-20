using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using OrdersManager.WPF.Services.Interfaces;
using OrdersManager.WPF.ViewModels;
using OrdersManager.WPF.Views.Windows;
using System.Collections.Generic;

namespace OrdersManager.WPF.Services
{
    public class OrderDialogService : IOrderDialog
    {
        public bool Edit(OrderModel orderModel, IEnumerable<Employee> employees, string title, bool enableSelectEmployee)
        {
            var editOrderVm= new OrderEditViewModel(orderModel, employees, title, enableSelectEmployee);
            var editOrderWindow = new OrderEditWindow
            {
                DataContext = editOrderVm
            };
            if (editOrderWindow.ShowDialog() != true) return false;

            return true;
        }
    }
}
