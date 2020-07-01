using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart_9b.Models;
using Microsoft.Extensions.Configuration;


namespace ShoppingCart_9b.DB
{
    public class ShoppingContext : DbContext
    {

        protected IConfiguration configuration;

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
        }

        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ActivationCode> ActivationCode { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ProductReview> ProductReview { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<User> User { get; set; }
    }
}
