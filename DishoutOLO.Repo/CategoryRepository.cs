using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishoutOLO.Repo
{
    public class CategoryRepository: Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(FoodOrderingContext orderingContext) :base (orderingContext)
        { 

        }
    }
}
