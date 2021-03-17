using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private string _title = "Главное окно";
        private readonly IRepository<Order> _orderRepository;

        public string Title
        {
            get => _title; set => Set(ref _title, value);            
        }
        public MainWindowViewModel(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
            var orders = _orderRepository.Items.Take(5).ToArray();
        }

    }
}
