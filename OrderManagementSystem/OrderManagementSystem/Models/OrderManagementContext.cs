using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;



namespace OrderManagementSystem.Models
{
    public class OrderManagementContext : DbContext
    {
        public OrderManagementContext(DbContextOptions<OrderManagementContext> options) : base(options)
        {

        }

        public DbSet<Users> Userss { get; set; }
        public DbSet<Products> Productss { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItemss { get; set; }
        public DbSet<Order> Orderss { get; set; }
        public DbSet<OrderDetails> OrderDetailss { get; set; }
    }
}
