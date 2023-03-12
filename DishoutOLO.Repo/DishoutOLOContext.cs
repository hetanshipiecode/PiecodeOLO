using DishoutOLO.Data;
using Microsoft.EntityFrameworkCore;

namespace DishoutOLO.Repo
{
    public class DishoutOLOContext : DbContext
    {
        public DishoutOLOContext(DbContextOptions<DishoutOLOContext> options) : base(options)
        {
        }
            
        public DbSet<Category> Categories { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DbSet<Customer> Customers { get; set; }  
    }
}
