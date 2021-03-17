using OrdersManager.DAL.Entityes.Base;
using System.Collections.Generic;

namespace OrdersManager.DAL.Entityes
{
    public class Department:NamedEntityBase
    {
        public int ManagerId { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
