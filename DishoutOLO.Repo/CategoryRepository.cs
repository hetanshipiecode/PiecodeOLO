using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
namespace DishoutOLO.Repo
{
    public class CategoryRepository: Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(DishoutOLOContext orderingContext) :base (orderingContext)
        { 

        }
    }
}
