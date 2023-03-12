using DishoutOLO.ViewModel.Helper;
using DishoutOLO.ViewModel;


namespace DishoutOLO.Service.Interface
{
    public interface IitemService
    {
        public DishoutOLOResponseModel AddOrUpdateItem(AddItemModel data);

        public DishoutOLOResponseModel DeleteItem(int data);
        public DataTableFilterModel GetItemList(DataTableFilterModel filter);

        public AddItemModel GetItem(int Id);

        public DishoutOLOResponseModel GetAllCategories();
    }
}
