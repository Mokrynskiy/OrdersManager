using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Services.Interfaces;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Поля
        private string _title = "Главное окно";        
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IRepository<Department> _departmentsRepository;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IEmployeeDialog _employeeDialog;
        private ViewModel _currentModel;
        #endregion

        #region Команды

        #region ShowEmployeesViewCommand (отображение представления сотрудников)
        private ICommand _showEmployeesViewCommand;
        public ICommand ShowEmployeesViewCommand => _showEmployeesViewCommand
            ??= new LambdaCommand(OnShowEmployeesViewCommanExecuted, CanShowEmployeeCommandExecute);
        private bool CanShowEmployeeCommandExecute() => true; 
        private void OnShowEmployeesViewCommanExecuted()
        {
            CurrentModel = new EmployeesViewModel(_employeesRepository, _ordersRepository, _departmentsRepository, _employeeDialog);
        }
        #endregion

        #region ShowDedartmentsViewCommand (отображение представления отделов(подразделений))
        private ICommand _showDepartmentsViewCommand;
        public ICommand ShowDepartmentsViewCommand => _showDepartmentsViewCommand
            ??= new LambdaCommand(OnShowDepartmentsViewCommanExecuted, CanShowDepartmentsCommandExecute);
        private bool CanShowDepartmentsCommandExecute() => true;
        private void OnShowDepartmentsViewCommanExecuted()
        {
            CurrentModel = new DepartmentsViewModel(_departmentsRepository, _employeesRepository);
        }
        #endregion

        #region ShowOrdersViewCommand (отображение представления заказов)
        private ICommand _showOrdersViewCommand;
        public ICommand ShowOrdersViewCommand => _showOrdersViewCommand
            ??= new LambdaCommand(OnShowOrdersViewCommanExecuted, CanShowOrdersCommandExecute);
        private bool CanShowOrdersCommandExecute() => true;
        private void OnShowOrdersViewCommanExecuted()
        {
            CurrentModel = new OrdersViewModel(_ordersRepository);
        }
        #endregion

        #endregion

        #region Свойства
        public string Title
        {
            get => _title; set => Set(ref _title, value);            
        }
        public ViewModel CurrentModel { get => _currentModel; private set => Set(ref _currentModel, value); }
        #endregion
        public MainWindowViewModel( IRepository<Employee> employeeRepository, IRepository<Department> departmentRepository,
            IRepository<Order> orderReposytory, IEmployeeDialog employeeDialog)
        {
            _employeeDialog = employeeDialog;
            _employeesRepository = employeeRepository;
            _departmentsRepository = departmentRepository;
            _ordersRepository = orderReposytory;
                 
        }

    }
}
