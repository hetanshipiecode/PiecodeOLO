            using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;


namespace DishoutOLO.Repo
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(DishoutOLOContext context) : base(context)
        {
        }
    }
}
