

using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;

namespace DishoutOLO.Service.Interface
{
    public interface ICategoryService
    {
        public DishoutOLOResponseModel AddOrUpdateCategory(CategoryModel data);
        public DishoutOLOResponseModel DeleteCategory(int data);
        public DataTableFilterModel GetCategoryList(DataTableFilterModel filter);

        public AddCategoryModel GetAddCategory(int Id);


    }
}
