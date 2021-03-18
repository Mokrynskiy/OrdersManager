using OrdersManager.Interfaces;

namespace OrdersManager.DAL.Entityes.Base
{
    public abstract class EntityBase: IEntity
    {
        public int Id { get; set; }
    }
}
