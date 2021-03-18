using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private string _title = "Главное окно";        
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IOrderService _orderService;
        private ViewModel _currentModel;
        
        #region ShowEmployeesViewCommand (отображение представления сотрудников)
        private ICommand _showEmployeesViewCommand;
        public ICommand ShowEmployeesViewCommand => _showEmployeesViewCommand
            ??= new LambdaCommand(OnShowEmployeesViewCommanExecuted, CanShowEmployeeCommandExecute);
        private bool CanShowEmployeeCommandExecute() => true; 
        private void OnShowEmployeesViewCommanExecuted()
        {
            CurrentModel = new EmployeesViewModel(_employeeRepository);
        }
        #endregion
        public string Title
        {
            get => _title; set => Set(ref _title, value);            
        }
        public ViewModel CurrentModel { get => _currentModel; private set => Set(ref _currentModel, value); }
        public MainWindowViewModel(IRepository<Employee> employeeRepository, IOrderService orderService)
        {
            _employeeRepository = employeeRepository;
            _orderService = orderService;            
        }

    }
}
