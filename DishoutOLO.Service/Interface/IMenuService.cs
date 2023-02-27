
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using static DishoutOLO.ViewModel.AddMenuModel;

namespace DishoutOLO.Service.Interface
{
    public interface IMenuService
    {
        public DishoutOLOResponseModel AddOrUpdateMenu(AddMenuModel data);

        public DishoutOLOResponseModel DeleteMenu(int data);
        public AddMenuModel GetAddMenu(int Id);

        public DataTableFilterModel GetMenuList(DataTableFilterModel filter);

       
    }
}
