using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class DepartmentsViewModel : ViewModel, INotifyPropertyChanged
    {
        private readonly IRepository<Department> _departmentsRepository;
        private readonly IRepository<Employee> _employeessRepository;
        private DepartmentModel selectedDepattment;
        private EmployeeModel selectedEmployee;
        public ObservableCollection<DepartmentModel> Departments { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public DepartmentModel SelectedDepartment {
            get
            {
                return this.selectedDepattment;
            }

            set
            {
                if (value != this.selectedDepattment)
                {
                    this.selectedDepattment = value;
                    NotifyPropertyChanged();
                }
            }
        }
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region LoadDepartmentsCommand (Загрузка данных о департаментах)        
        private ICommand _loadDepartmentsCommand;
        public ICommand LoadDepartmentsCommand => _loadDepartmentsCommand
            ??= new LambdaCommand(LoadDepartmentsCommanExecuted, LoadDepartmentsCommandExecute);
        private bool LoadDepartmentsCommandExecute() => true;
        private void LoadDepartmentsCommanExecuted()
        {
            Departments.Clear();
            ObservableCollection<EmployeeModel> emp = new ObservableCollection<EmployeeModel>();
            emp.Clear();
            var dept = _departmentsRepository.Items.ToArray();            
            foreach (var item in dept)
            {
                emp.Clear();
                foreach (var i in item.Employees)
                {
                    emp.Add(new EmployeeModel {
                        EmployeeSurname = i.Surname,
                        EmployeeName = i.Name,
                        EmployeePatronymic = i.Patronymic,
                        EmployeeGender = i.Gender, 
                        EmployeeId = i.Id, 
                        EmployeeBirthdey = i.Birthday                     
                        });
                }
                var mgr = (from m in item.Employees where m.Id == item.ManagerId select m).FirstOrDefault();
                EmployeeModel manager = null;
                if (mgr != null)
                {
                    manager = new EmployeeModel { EmployeeId = mgr.Id, EmployeeSurname = mgr.Surname, EmployeeName = mgr.Name, EmployeePatronymic = mgr.Patronymic, EmployeeBirthdey = mgr.Birthday, EmployeeGender = mgr.Gender };
                }
                
                DepartmentModel department = new DepartmentModel {
                    DepartmentId = item.Id,
                    DepartmentName = item.Name,
                    ManagerId = item.ManagerId,
                    Manager = manager,
                    Employees = new ObservableCollection<EmployeeModel>(emp)
                };
                Departments.Add(department);                
            }
            SelectedDepartment = Departments.FirstOrDefault();
        }
        #endregion

        #region DeleteDepartmentCommand (Удаление данных об отделе)        
        private ICommand _deleteDepartmentCommand;
        public ICommand DeleteDepartmentCommand => _deleteDepartmentCommand
            ??= new LambdaCommand(DeleteDepartmentCommanExecuted, DeleteDepartmentCommandExecute);
        private bool DeleteDepartmentCommandExecute() => true;
        private void DeleteDepartmentCommanExecuted()
        {
            if (selectedDepattment is null)
            {
                MessageBox.Show("Для удаления отдела необходимо выделить запись в списке отделов!!!", "Ошибка!!!");
            }
            else
            {
                if (MessageBox.Show($"Удаление отдела повлечет за собой удаление связанных с ним сотрудников и заказов!!! \n" +
                    $"Вы действительно хотите удалить отдел {selectedDepattment.DepartmentName}?",
                    "Внимание!!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _departmentsRepository.Remove(selectedDepattment.DepartmentId);
                        Departments.Remove(selectedDepattment);
                        SelectedDepartment = Departments.FirstOrDefault();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        #endregion
        #region EditDepartmentCommand (Редактирование данных о заказе)        
        private ICommand _editDepartmentCommand;
        public ICommand EditDepartmentCommand => _editDepartmentCommand
            ??= new LambdaCommand(EditDepartmentCommanExecuted, EditDepartmentCommandExecute);
        private bool EditDepartmentCommandExecute() => true;
        private void EditDepartmentCommanExecuted()
        {

        }
        #endregion
        #region AddDepartmentCommand (Добавление заказа)        
        private ICommand _addDepartmentCommand;
        public ICommand AddDepartmentCommand => _addDepartmentCommand
            ??= new LambdaCommand(AddDepartmentCommanExecuted, AddDepartmentCommandExecute);
        private bool AddDepartmentCommandExecute() => true;
        private void AddDepartmentCommanExecuted()
        {

        }
        #endregion

        #region DeleteEmployeeCommand (Удаление данных о соткгднике)        
        private ICommand _deleteEmployeeCommand;
        public ICommand DeleteEmployeeCommand => _deleteEmployeeCommand
            ??= new LambdaCommand(DeleteEmployeeCommanExecuted, DeleteEmployeeCommandExecute);
        private bool DeleteEmployeeCommandExecute() => true;
        private void DeleteEmployeeCommanExecuted()
        {
            if (selectedEmployee is null)
            {
                MessageBox.Show("Для удаления сотрудника необходимо выделить запись в таблице сотрудников!!!", "Ошибка!!!");
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
                        _employeessRepository.Remove(selectedEmployee.EmployeeId);
                        selectedDepattment.Employees.Remove(selectedEmployee);
                        selectedEmployee = selectedDepattment.Employees.FirstOrDefault();
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

        public DepartmentsViewModel(IRepository<Department> departmentsRepository, IRepository<Employee> employeeRepository) 
        {
            _departmentsRepository = departmentsRepository;
            _employeessRepository = employeeRepository;
            Departments = new ObservableCollection<DepartmentModel>();
                      
        }
    }
}
