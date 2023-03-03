using AutoMapper;
using DishoutOLO.Data;
using DishoutOLO.ViewModel;

namespace DishoutOLO
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()

        {   
            CreateMaps();

        }
        private void CreateMaps()
        {
            CreateMap<Category,AddCategoryModel>().ReverseMap();
               
            CreateMap<Item,AddItemModel>()
                .ForMember(entity => entity.File, options => options.Ignore()).ReverseMap();

            CreateMap<Menu, AddMenuModel>()
             .ForMember(entity => entity.File, options => options.Ignore()).ReverseMap();
        }

    }

}   


