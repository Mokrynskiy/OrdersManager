using Microsoft.EntityFrameworkCore;
using OrdersManager.DAL.Context;
using OrdersManager.DAL.Entityes;
using System.Linq;

namespace OrdersManager.DAL.Repositoryes
{
    internal class OrdersRepository : DbRepository<Order>
    {
        public override IQueryable<Order> Items => base.Items.Include(item => item.Author);
        public OrdersRepository(OrdersManagerDbContext db): base(db) 
        {

        }
    }
}
