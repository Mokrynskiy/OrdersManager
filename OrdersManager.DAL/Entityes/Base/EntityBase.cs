using OrdersManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.DAL.Entityes.Base
{
    public abstract class EntityBase: IEntity
    {
        public int Id { get; set; }
    }
}
