using Microsoft.EntityFrameworkCore;
using Shop.Model;

namespace Shop.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=Shop;User ID=sa;Password=1q2w3e4r@#$");
    }
}
