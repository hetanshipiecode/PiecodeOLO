using DishoutOLO.Data;
using Microsoft.EntityFrameworkCore;

namespace DishoutOLO.Repo
{
    public class DishoutOLOContext : DbContext
    {
        public DishoutOLOContext(DbContextOptions<DishoutOLOContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    var connectionString = configuration.GetConnectionString("ConnectDB");
        //    optionsBuilder.UseSqlServer(connectionString);
        //}

        public DbSet<Category> Categories { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Item> Items { get; set; }


    }
}
