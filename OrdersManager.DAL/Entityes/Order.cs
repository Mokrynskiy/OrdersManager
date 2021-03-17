using OrdersManager.DAL.Entityes.Base;
using System;
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
        public virtual Employee Author { get; set; }
    }
}
