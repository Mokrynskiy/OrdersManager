using Microsoft.EntityFrameworkCore;
using OrdersManager.DAL.Context;
using OrdersManager.DAL.Entityes;
using System.Linq;

namespace OrdersManager.DAL.Repositoryes
{
    internal class EmploeesRepository : DbRepository<Employee>
    {
        public override IQueryable<Employee> Items => base.Items.Include(item => item.Department).Include(item =>item.Orders);
        public EmploeesRepository(OrdersManagerDbContext db) : base(db)
        {

        }
    }
}
