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
using System.Windows;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class DepartmentsViewModel : ViewModel, INotifyPropertyChanged
    {
        private readonly IRepository<Department> _departmentsRepository;
        private readonly IRepository<Employee> _employeesRepository;
        private DepartmentModel selectedDepattment;
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


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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
                        EmployeeName = i.Name, 
                        EmployeeGender = i.Gender, 
                        EmployeeId = i.Id, 
                        EmployeePatronymic = i.Patronymic, 
                        EmployeeSurname = i.Surname});
                }
                
                DepartmentModel department = new DepartmentModel {
                    DepartmentId = item.Id,
                    DepartmentName = item.Name,
                    ManagerId = item.ManagerId,
                    Employees = new ObservableCollection<EmployeeModel>(emp)
                };
                Departments.Add(department);                
            }            
        }
        #endregion     
        
        public DepartmentsViewModel(IRepository<Department> departmentsRepository) 
        {
            _departmentsRepository = departmentsRepository;            
            Departments = new ObservableCollection<DepartmentModel>();
                      
        }
    }
}
