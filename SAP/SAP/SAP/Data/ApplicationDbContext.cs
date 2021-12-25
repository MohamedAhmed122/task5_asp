using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SAP.Data.Models;
using SAP.Data.Models.Catalogue;

namespace SAP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Attributes> Attributes { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<ItemToCart> ItemToCarts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ItemToOrder> ItemToOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemToCart>().HasKey(x => new { x.CartId, x.ItemId });
            modelBuilder.Entity<ItemToOrder>().HasKey(x => new { x.OrderId, x.ItemId });
        }
    }
}
