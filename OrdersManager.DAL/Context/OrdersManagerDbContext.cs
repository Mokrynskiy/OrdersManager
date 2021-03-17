using Microsoft.EntityFrameworkCore;
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
    }
}
