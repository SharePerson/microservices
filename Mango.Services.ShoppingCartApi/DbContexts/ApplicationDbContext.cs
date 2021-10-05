using Mango.Services.ShoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartApi.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<CardDetail> CardDetail { get; set; }
        public DbSet<CartHeader> CartHeader { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
