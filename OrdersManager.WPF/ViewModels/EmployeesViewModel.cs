
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class EmployeesViewModel : ViewModel, INotifyPropertyChanged
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Order> _orderRepository;
        private EmployeeModel selectedEmployee;
        private OrderModel selectedOrder;
        public ObservableCollection<EmployeeModel> Employees { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public EmployeeModel SelectedEmployee
        {
            get
            {
                return this.selectedEmployee;
            }

            set
            {
                if (value != this.selectedEmployee)
                {
                    this.selectedEmployee = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public OrderModel SelectedOrder
        {
            get
            {
                return this.selectedOrder;
            }

            set
            {
                if (value != this.selectedOrder)
                {
                    this.selectedOrder = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region LoadEmployeesCommand (Загрузка данных о сотрудниках)        
        private ICommand _loadEmployeesCommand;
        public ICommand LoadEmployeesCommand => _loadEmployeesCommand
            ??= new LambdaCommand(LoadEmployeesCommanExecuted, LoadEmployeesCommandExecute);
        private bool LoadEmployeesCommandExecute() => true;
        private void LoadEmployeesCommanExecuted()
        {
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
                        OrderId = i.Id,
                        OrderDate = i.Date,
                        Contractor = i.Contractor
                    });
                }
               
                EmployeeModel employee = new EmployeeModel
                {
                    EmployeeId = item.Id,
                    EmployeeSurname = item.Surname,
                    EmployeeName = item.Name,
                    EmployeePatronymic = item.Patronymic,
                    EmployeeBirthdey = item.Birthday,
                    EmployeeGender = item.Gender,
                    Department = new DepartmentModel { DepartmentId = item.Department.Id, DepartmentName = item.Department.Name},
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
            if (selectedOrder is null)
            {
                MessageBox.Show("Для удаления заказа необходимо выделить запись в таблице Заказы!!!", "Ошибка!!!");
            }
            else
            {
                if (MessageBox.Show($"Вы действительно хотите удалить заказ\n " +
                    $"{selectedOrder.OrderDate.ToShortDateString()} - {SelectedOrder.Contractor}", 
                    "Внимание!!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _orderRepository.Remove(selectedOrder.OrderId);
                        selectedEmployee.Orders.Remove(SelectedOrder);
                        selectedOrder = selectedEmployee.Orders.FirstOrDefault();
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
            if (selectedEmployee is null)
            {
                MessageBox.Show("Для удаления сотрудника необходимо выделить запись в списке сотрудников!!!", "Ошибка!!!");
            }
            else
            {
                if (MessageBox.Show($"Удаление сотрудника повлечет за собой удаление всех связанных с этим сотрудником заказов!!! \n" +
                    $"Вы действительно хотите удалить сотрудника: " +
                    $"{selectedEmployee.EmployeeSurname}  {selectedEmployee.EmployeeName} {selectedEmployee.EmployeePatronymic}?",
                    "Внимание!!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _employeeRepository.Remove(selectedEmployee.EmployeeId);
                        Employees.Remove(selectedEmployee);
                        selectedEmployee = Employees.FirstOrDefault();
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
        public ICommand EditEmployeeCommand => _editOrderCommand
            ??= new LambdaCommand(EditEmployeeCommanExecuted, EditEmployeeCommandExecute);
        private bool EditEmployeeCommandExecute() => true;
        private void EditEmployeeCommanExecuted()
        {

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
        public EmployeesViewModel(IRepository<Employee> employeeRepository, IRepository<Order> orderRepository)
        {
            _employeeRepository = employeeRepository;
            _orderRepository = orderRepository;
            Employees = new ObservableCollection<EmployeeModel>();
        }
    }
}
