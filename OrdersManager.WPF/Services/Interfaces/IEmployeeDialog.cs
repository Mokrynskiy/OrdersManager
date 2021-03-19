using OrdersManager.DAL.Entityes;
using OrdersManager.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.Services.Interfaces
{
    public interface IEmployeeDialog
    {
        public bool Edit(EmployeeModel employeModel);
    }
}
