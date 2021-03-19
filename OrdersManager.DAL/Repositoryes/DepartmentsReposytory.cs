using Microsoft.EntityFrameworkCore;
using OrdersManager.DAL.Context;
using OrdersManager.DAL.Entityes;
using System.Linq;

namespace OrdersManager.DAL.Repositoryes
{
    internal class DepartmentsReposytory : DbRepository<Department>
    {
        public override IQueryable<Department> Items => base.Items.Include(item => item.Employees);
        public DepartmentsReposytory (OrdersManagerDbContext db) : base(db)
        {

        }
    }
}
