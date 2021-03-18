
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class EmployeesViewModel : ViewModel, INotifyPropertyChanged
    {
        private readonly IRepository<Employee> _employeeRepository;
        private EmployeeModel selectedEmployee;
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
        }
        #endregion
        public EmployeesViewModel(IRepository<Employee> employeeRepository)
        {
            this._employeeRepository = employeeRepository;
            Employees = new ObservableCollection<EmployeeModel>();
        }
    }
}
