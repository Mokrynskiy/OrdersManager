using Microsoft.EntityFrameworkCore;
using OrdersManager.DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.DAL.Context
{
    public class OrdersManagerDbContext: DbContext
    {
        public OrdersManagerDbContext(DbContextOptions<OrdersManagerDbContext> options): base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
