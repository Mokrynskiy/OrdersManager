using OrdersManager.DAL.Entityes.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrdersManager.DAL.Entityes
{
    public class Order:EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Contractor { get; set; }
        [Required]
        [Column(TypeName ="Date")]
        public DateTime Date { get; set; }        
    }
    public class Department:NamedEntityBase
    {
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
    public class Employee:PersonEntityBase
    {
        [Required]
        [Column(TypeName ="Date")]
        public DateTime Birthday { get; set; }
        public virtual Department Department { get; set; }
    }
}
