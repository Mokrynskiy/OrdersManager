using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Services.interfaces;
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
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IOrderService _orderService;

        public string Title
        {
            get => _title; set => Set(ref _title, value);            
        }
        public MainWindowViewModel(IRepository<Employee> employeeRepository, IOrderService orderService)
        {
            _employeeRepository = employeeRepository;
            _orderService = orderService;            
        }

    }
}
