using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;

namespace DishoutOLO.Service.Interface
{
    public interface ICategoryService
    {
        public DishoutOLOResponseModel AddOrUpdateCategory(AddCategoryModel data);
        
        public DishoutOLOResponseModel DeleteCategory(int data);
        public DataTableFilterModel GetCategoryList(DataTableFilterModel filter);

        public AddCategoryModel GetCategory(int Id);

        public DishoutOLOResponseModel GetAllCategories();

    }
}
