using Microsoft.EntityFrameworkCore;
using OrdersManager.DAL.Entityes;

namespace OrdersManager.DAL.Context
{
    public class OrdersManagerDbContext: DbContext
    {
        public OrdersManagerDbContext(DbContextOptions<OrdersManagerDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.Department)
                .WithMany(t => t.Employees)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .HasOne(p => p.Author)
                .WithMany(t => t.Orders)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
