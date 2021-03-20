using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using OrdersManager.WPF.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class DepartmentsViewModel : ViewModel
    {
        private readonly IRepository<Department> _departmentsRepository;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IDepartmentDialog _departmentDialog;
        private readonly IEmployeeDialog _employeeDialog;
        private ObservableCollection<DepartmentModel> _departments;
        private DepartmentModel _selectedDepattment;
        private EmployeeModel _selectedEmployee;
        public ObservableCollection<DepartmentModel> Departments { get =>_departments; set=>Set(ref _departments,value); }
        public DepartmentModel SelectedDepartment { get => _selectedDepattment; set => Set(ref _selectedDepattment, value); }
        public EmployeeModel SelectedEmployee { get => _selectedEmployee; set => Set(ref _selectedEmployee, value);  }


        #region LoadDepartmentsCommand (Загрузка данных о департаментах)        
        private ICommand _loadDepartmentsCommand;
        public ICommand LoadDepartmentsCommand => _loadDepartmentsCommand
            ??= new LambdaCommand(LoadDepartmentsCommanExecuted, LoadDepartmentsCommandExecute);
        private bool LoadDepartmentsCommandExecute() => true;
        private void LoadDepartmentsCommanExecuted()
        {
            Departments = new ObservableCollection<DepartmentModel>();
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
                        Surname = i.Surname,
                        Name = i.Name,
                        Patronymic = i.Patronymic,
                        Gender = i.Gender, 
                        Id = i.Id, 
                        Birthdey = i.Birthday,     
                        Department = new DepartmentModel { Id = item.Id, DepartmentName = i.Name}
                        });
                }
                var mgr = (from m in item.Employees where m.Id == item.ManagerId select m).FirstOrDefault();
                EmployeeModel manager = null;
                if (mgr != null)
                {
                    manager = new EmployeeModel { Id = mgr.Id, Surname = mgr.Surname, Name = mgr.Name, Patronymic = mgr.Patronymic, Birthdey = mgr.Birthday, Gender = mgr.Gender };
                }
                
                DepartmentModel department = new DepartmentModel {
                    Id = item.Id,
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
            if (_selectedDepattment is null)
            {
                MessageBox.Show("Для удаления отдела необходимо выделить запись в списке отделов!!!", "Ошибка!!!");
            }
            else
            {
                if (MessageBox.Show($"Удаление отдела повлечет за собой удаление связанных с ним сотрудников и заказов!!! \n" +
                    $"Вы действительно хотите удалить отдел {_selectedDepattment.DepartmentName}?",
                    "Внимание!!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _departmentsRepository.Remove(_selectedDepattment.Id);
                        Departments.Remove(_selectedDepattment);
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
            string title = "Редактирование данных об отделе";
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Необходимо выделить запись для редактирования!!!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dept = SelectedDepartment;
            if (_departmentDialog.Edit(dept, title))
            {
                var dep = _departmentsRepository.GetById(dept.Id);
                _departmentsRepository.Update(dep);
                SelectedDepartment = dept;
                return;
            }

        }
        #endregion

        #region AddDepartmentCommand (Добавление отдела)        
        private ICommand _addDepartmentCommand;
        public ICommand AddDepartmentCommand => _addDepartmentCommand
            ??= new LambdaCommand(AddDepartmentCommanExecuted, AddDepartmentCommandExecute);
        private bool AddDepartmentCommandExecute() => true;
        private void AddDepartmentCommanExecuted()
        {
            string title = "Добавление нового отдела";
            
            var dept = new DepartmentModel();
            if (_departmentDialog.Edit(dept, title))
            {
                if (dept.DepartmentName == null)
                {
                    MessageBox.Show("Название отдела не может быть пустым", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }               
                dept.Id = _departmentsRepository.Add(new Department { Name = dept.DepartmentName}).Id;
                Departments.Add(dept);
                SelectedDepartment = dept;
                MessageBox.Show($"Отдел {dept.DepartmentName} успешно создан. \n Для дальнейшего редактирования создайте пользователей и назначте руководителя отдела." , "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Information) ;
                return;
            }

        }
        #endregion

        #region DeleteEmployeeCommand (Удаление данных о сотруднике)        
        private ICommand _deleteEmployeeCommand;
        public ICommand DeleteEmployeeCommand => _deleteEmployeeCommand
            ??= new LambdaCommand(DeleteEmployeeCommanExecuted, DeleteEmployeeCommandExecute);
        private bool DeleteEmployeeCommandExecute() => true;
        private void DeleteEmployeeCommanExecuted()
        {
            if (_selectedEmployee is null)
            {
                MessageBox.Show("Для удаления сотрудника необходимо выделить запись в таблице сотрудников!!!", "Ошибка!!!");
            }
            else
            {
                if (MessageBox.Show($"Удаление сотрудника повлечет за собой удаление всех связанных с этим сотрудником заказов!!! \n" +
                    $"Вы действительно хотите удалить сотрудника: " +
                    $"{_selectedEmployee.Surname}  {_selectedEmployee.Name} {_selectedEmployee.Patronymic}?",
                    "Внимание!!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _employeesRepository.Remove(_selectedEmployee.Id);
                        _selectedDepattment.Employees.Remove(_selectedEmployee);
                        _selectedEmployee = _selectedDepattment.Employees.FirstOrDefault();
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
            if (SelectedEmployee == null)
            {
                MessageBox.Show("Необходимо выделить запись для редактирования!!!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var Employee = SelectedEmployee;
            var Departments = _departmentsRepository.Items.ToArray();
            if (_employeeDialog.Edit(Employee, Departments, title, false))
            {
                var emp = _employeesRepository.GetById(Employee.Id);
                emp.Surname = Employee.Surname;
                emp.Name = Employee.Name;
                emp.Patronymic = Employee.Patronymic;
                emp.Gender = Employee.Gender;
                emp.Birthday = Employee.Birthdey;
                emp.Department = _departmentsRepository.GetById(Employee.Department.Id);
                _employeesRepository.Update(emp);
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
            string title = "Добавление нового сотрудника";
            var Empl = new EmployeeModel();
            Empl.Department = SelectedDepartment;            
            Empl.Birthdey = DateTime.Now.AddYears(-18);
            var Departments = _departmentsRepository.Items.ToArray();
            if (_employeeDialog.Edit(Empl, Departments, title, false))
            {
                if (Empl.Surname != null && Empl.Name != null && Empl.Patronymic != null && Empl.Department != null)
                {
                    var emp = new Employee();
                    emp.Surname = Empl.Surname;
                    emp.Name = Empl.Name;
                    emp.Patronymic = Empl.Patronymic;
                    emp.Gender = Empl.Gender;
                    emp.Birthday = Empl.Birthdey;
                    var dept = _departmentsRepository.GetById(Empl.Department.Id);
                    emp.Department = dept;
                    _employeesRepository.Add(emp);
                    Empl.Id = emp.Id;
                    SelectedDepartment.Employees.Add(Empl);
                    SelectedEmployee = Empl;
                    return;
                }
                MessageBox.Show("Для добавдения сотрудника необходимо заполнить все поля!!!",
                    "Ошибка добавления сотрудника!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region AddManagerCommand (Назначение руководителя отдела)        
        private ICommand _addManagerCommand;
        public ICommand AddManagerCommand => _addManagerCommand
            ??= new LambdaCommand(AddManagerCommanExecuted, AddManagerCommandExecute);
        private bool AddManagerCommandExecute() => true;
        private void AddManagerCommanExecuted()
        {
            if (SelectedEmployee == null)
            {
                MessageBox.Show("Необходимо выделить сотрудника для назначения!!!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var selDept = SelectedDepartment;
            var dept = _departmentsRepository.GetById(selDept.Id);
            dept.ManagerId = SelectedEmployee.Id;
            _departmentsRepository.Update(dept);
            selDept.ManagerId = dept.ManagerId;
            this.LoadDepartmentsCommand.Execute(null);            
            SelectedDepartment = (from d in Departments where d.Id == selDept.Id select d).FirstOrDefault();


        }
        #endregion

        public DepartmentsViewModel(IRepository<Department> departmentsRepository, IRepository<Employee> employeeRepository, IDepartmentDialog departmentDialog, IEmployeeDialog employeeDialog) 
        {
            _employeeDialog = employeeDialog;
            _departmentDialog = departmentDialog;
            _departmentsRepository = departmentsRepository;
            _employeesRepository = employeeRepository;            
                      
        }
    }
}
