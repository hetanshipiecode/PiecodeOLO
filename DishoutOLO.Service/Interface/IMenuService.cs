
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
namespace DishoutOLO.Service.Interface
{
    public interface IMenuService
    {
        public DishoutOLOResponseModel AddOrUpdateMenu(AddMenuModel data,string imgPath="");

        public DishoutOLOResponseModel DeleteMenu(int data);
        public AddMenuModel GetAddMenu(int Id);

        public DataTableFilterModel GetMenuList(DataTableFilterModel filter);

       
    }
}
