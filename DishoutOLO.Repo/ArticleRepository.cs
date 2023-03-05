using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
namespace DishoutOLO.Repo
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(DishoutOLOContext orderingContext) : base(orderingContext)
        {

        }
    }
}
