using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
    
namespace DishoutOLO.Repo
{
    public class ItemRepository : Repository<Item>, IItemRepositrory
    {
        public ItemRepository(DishoutOLOContext itemContext) : base(itemContext )
        {

        }
    }
}
