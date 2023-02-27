using DishoutOLO.Data;
using Microsoft.EntityFrameworkCore;


namespace DishoutOLO.Repo
{
    public class FoodOrderingContext:DbContext
    {

        public FoodOrderingContext(DbContextOptions<FoodOrderingContext> options) : base(options)
        {
        }
            public DbSet<Category> Categories { get; set; }

        public DbSet<Menu> Menus { get; set; }
    }
}
