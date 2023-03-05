using DishoutOLO.ViewModel.Helper;
using DishoutOLO.ViewModel;

namespace DishoutOLO.Service.Interface
{
    public interface IArticleService
    {
        public DishoutOLOResponseModel AddOrUpdateArticle(AddArticleModel data);

        public DishoutOLOResponseModel DeleteArticle(int data);
        public DataTableFilterModel GetArticleList(DataTableFilterModel filter);

        public AddArticleModel GetArticle(int Id);
    }
}
