using OrdersManager.DAL.Entityes.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersManager.DAL.Entityes
{
    public class Employee:PersonEntityBase
    {
        [Required]
        [Column(TypeName ="Date")]
        public DateTime Birthday { get; set; }
        public virtual Department Department { get; set; }
    }
}
