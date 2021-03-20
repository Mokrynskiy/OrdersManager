﻿using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathCore.WPF.Commands;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class EmployeeEditViewModel : ViewModel
    {
       
        private EmployeeModel _employee;
        public string Title { get; set; }
        public IEnumerable<Department> Departments { get; private set; }
        public EmployeeModel Employee { get => _employee; set => Set(ref _employee, value); }
        public IEnumerable<Gender> Genders { get => Enum.GetValues(typeof(Gender)).Cast<Gender>(); }

        public EmployeeEditViewModel(EmployeeModel employee, IEnumerable<Department> departments, string title)
        {           
            Employee = employee;
            Departments = departments;
            Title = title;
        }

        #region SetDepartmentCommand (Редактирование данных о сотрудние)        
        private ICommand _setDepartmentCommand;
        public ICommand SetDepartmentCommand => _setDepartmentCommand
            ??= new LambdaCommand(SetDepartmentCommanExecuted, SetDepartmentCommandExecute);
        private bool SetDepartmentCommandExecute() => true;
        private void SetDepartmentCommanExecuted()
        {

            var dept = Departments.Where(d => d.Id == Employee.Department.Id).FirstOrDefault();
            Employee.Department = new DepartmentModel { Id = dept.Id, DepartmentName = dept.Name, ManagerId = dept.ManagerId };
        }
        #endregion


    }
}
