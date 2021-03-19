using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using OrdersManager.WPF.Services;
using OrdersManager.WPF.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class EmployeesViewModel : ViewModel
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IEmployeeDialog _employeeDialog;
        private ObservableCollection<EmployeeModel> _employees;
        private EmployeeModel _selectedEmployee;
        private OrderModel _selectedOrder;
        public ObservableCollection<EmployeeModel> Employees { get => _employees; set => Set(ref _employees, value); } 
        public EmployeeModel SelectedEmployee { get => _selectedEmployee; set => Set(ref _selectedEmployee, value); }
        public OrderModel SelectedOrder { get => _selectedOrder; set => Set(ref _selectedOrder, value); }

        #region LoadEmployeesCommand (Загрузка данных о сотрудниках)        
        private ICommand _loadEmployeesCommand;
        public ICommand LoadEmployeesCommand => _loadEmployeesCommand
            ??= new LambdaCommand(LoadEmployeesCommanExecuted, LoadEmployeesCommandExecute);
        private bool LoadEmployeesCommandExecute() => true;
        private void LoadEmployeesCommanExecuted()
        {
            Employees = new ObservableCollection<EmployeeModel>();
            Employees.Clear();
            ObservableCollection<OrderModel> orders = new ObservableCollection<OrderModel>();            
            var empl = _employeeRepository.Items.ToArray();
            foreach (var item in empl)
            {
                orders.Clear();
                foreach (var i in item.Orders)
                {
                    orders.Add(new OrderModel
                    {
                        Id = i.Id,
                        Date = i.Date,
                        Contractor = i.Contractor
                    });
                }
               
                EmployeeModel employee = new EmployeeModel
                {
                    Id = item.Id,
                    Surname = item.Surname,
                    Name = item.Name,
                    Patronymic = item.Patronymic,
                    Birthdey = item.Birthday,
                    Gender = item.Gender,
                    Department = new DepartmentModel { Id = item.Department.Id, DepartmentName = item.Department.Name},
                    Orders = new ObservableCollection<OrderModel>(orders)
                };
                Employees.Add(employee);
            }
            SelectedEmployee = Employees.FirstOrDefault();
            SelectedOrder = SelectedEmployee.Orders.FirstOrDefault();
        }
        #endregion

        #region DeleteOrderCommand (Удаление данных о заказе)        
        private ICommand _deleteOrderCommand;
        public ICommand DeleteOrderCommand => _deleteOrderCommand
            ??= new LambdaCommand(DeleteOrderCommanExecuted, DeleteOrderCommandExecute);
        private bool DeleteOrderCommandExecute() => true;
        private void DeleteOrderCommanExecuted()
        {
            if (SelectedOrder is null)
            {
                MessageBox.Show("Для удаления заказа необходимо выделить запись в таблице Заказы!!!", "Ошибка!!!");
            }
            else
            {
                if (MessageBox.Show($"Вы действительно хотите удалить заказ\n " +
                    $"{SelectedOrder.Date.ToShortDateString()} - {SelectedOrder.Contractor}", 
                    "Внимание!!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _orderRepository.Remove(SelectedOrder.Id);
                        SelectedEmployee.Orders.Remove(SelectedOrder);
                        SelectedOrder = SelectedEmployee.Orders.FirstOrDefault();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        #endregion
        #region EditOrderCommand (Редактирование данных о заказе)        
        private ICommand _editOrderCommand;
        public ICommand EditOrderCommand => _editOrderCommand
            ??= new LambdaCommand(EditOrderCommanExecuted, EditOrderCommandExecute);
        private bool EditOrderCommandExecute() => true;
        private void EditOrderCommanExecuted()
        {

        }
        #endregion
        #region AddOrderCommand (Добавление заказа)        
        private ICommand _addOrderCommand;
        public ICommand AddOrderCommand => _addOrderCommand
            ??= new LambdaCommand(AddOrderCommanExecuted, AddOrderCommandExecute);
        private bool AddOrderCommandExecute() => true;
        private void AddOrderCommanExecuted()
        {

        }
        #endregion

        #region DeleteEmployeeCommand (Удаление данных о сотруднике)        
        private ICommand _deleteEmployeeCommand;
        public ICommand DeleteEmployeeCommand => _deleteEmployeeCommand
            ??= new LambdaCommand(DeleteEmployeeCommanExecuted, DeleteEmployeeCommandExecute);
        private bool DeleteEmployeeCommandExecute() => true;
        private void DeleteEmployeeCommanExecuted()
        {
            if (SelectedEmployee is null)
            {
                MessageBox.Show("Для удаления сотрудника необходимо выделить запись в списке сотрудников!!!", "Ошибка!!!");
            }
            else
            {
                if (MessageBox.Show($"Удаление сотрудника повлечет за собой удаление всех связанных с этим сотрудником заказов!!! \n" +
                    $"Вы действительно хотите удалить сотрудника: " +
                    $"{SelectedEmployee.Surname}  {SelectedEmployee.Name} {SelectedEmployee.Patronymic}?",
                    "Внимание!!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _employeeRepository.Remove(SelectedEmployee.Id);
                        Employees.Remove(SelectedEmployee);
                        SelectedEmployee = Employees.FirstOrDefault();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        #endregion
        #region EditEmployeeCommand (Редактирование данных о сотрудние)        
        private ICommand _editEmployeeCommand;
        public ICommand EditEmployeeCommand => _editEmployeeCommand
            ??= new LambdaCommand(EditEmployeeCommanExecuted, EditEmployeeCommandExecute);
        private bool EditEmployeeCommandExecute() => true;
        private void EditEmployeeCommanExecuted()
        {
            string title = "Редактирование данных о сотруднике";
            var Employee = SelectedEmployee;
            var Departments = _departmentRepository.Items.ToArray();
            if (_employeeDialog.Edit(Employee, Departments, title))
            {
                var emp = _employeeRepository.GetById(Employee.Id);
                emp.Surname = Employee.Surname;
                emp.Name = Employee.Name;
                emp.Patronymic = Employee.Patronymic;
                emp.Gender = Employee.Gender;
                emp.Birthday = Employee.Birthdey;
                emp.Department = _departmentRepository.GetById(Employee.Department.Id);
                _employeeRepository.Update(emp);
                SelectedEmployee = null;
                SelectedEmployee = Employee;
                return;
            } 
            
        }
        #endregion
        #region AddEmployeeCommand (Добавление нового сотрудника)        
        private ICommand _addEmployeeCommand;
        public ICommand AddEmployeeCommand => _addEmployeeCommand
            ??= new LambdaCommand(AddEmployeeCommanExecuted, AddEmployeeCommandExecute);
        private bool AddEmployeeCommandExecute() => true;
        private void AddEmployeeCommanExecuted()
        {

        }
        #endregion
        public EmployeesViewModel(IRepository<Employee> employeeRepository, IRepository<Order> orderRepository, IRepository<Department>departmentRepository , IEmployeeDialog employeeDialog)
        {
            _employeeDialog = employeeDialog;
            _employeeRepository = employeeRepository;
            _orderRepository = orderRepository;
            _departmentRepository = departmentRepository;
        }
    }
}
