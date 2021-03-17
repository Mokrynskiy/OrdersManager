using System.ComponentModel.DataAnnotations;

namespace OrdersManager.DAL.Entityes.Base
{
    public abstract class NamedEntityBase:EntityBase
    {
        [Required]
        public string Name { get; set; }
    }
}
