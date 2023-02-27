using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using AutoMapper;

using static DishoutOLO.ViewModel.DishoutOLOResponseModel;

namespace DishoutOLO.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private IRepository<Menu> _menuRepository;
        public MenuService(IRepository<Menu> menuRepository,  IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public DishoutOLOResponseModel AddOrUpdateMenu(AddMenuModel data)
        {
            try
            {
                var categoryresponse = _menuRepository.GetAllAsQuerable().FirstOrDefault(x => x.IsActive == false && (x.MenuName.ToLower() == data.MenuName.ToLower()));

                var response = new DishoutOLOResponseModel();

                if (categoryresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (categoryresponse.MenuName.ToLower() == data.MenuName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "MenuName", ErrorDescription = "Menu already exist" });
                    }

                }
                if (data.Id == 0)
                {
                    
                    Menu tblMenu = _mapper.Map<AddMenuModel,Menu>(data); 
                   tblMenu.CreationDate=DateTime.Now;
                    tblMenu.IsActive= true;

                    if (data.File != null)
                    {
                        string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(data.File.FileName)}";
                        string path = Path.GetFullPath("Content/Menu", fileName);
                        Utility.SaveFile(data.File, path);
                        tblMenu.Image = fileName;
                    }
                    _menuRepository.Insert(tblMenu);
                }
                else
                {
                    Menu chk = _menuRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    chk = _mapper.Map<AddMenuModel, Menu>(data);
                    if (data.File != null)
                    {
                        string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(data.File.FileName)}";
                        string path = Path.GetFullPath("Content/Menu", fileName);
                        
                        Utility.SaveFile(data.File, path);
                        chk.Image = fileName;
                    }
                    _menuRepository.Update(chk);    

                }
                return new DishoutOLOResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "category") : string.Format(Constants.UpdatedSuccessfully, "category") };
            }
            catch (Exception ex)
            {
                return new DishoutOLOResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }

        public DishoutOLOResponseModel DeleteMenu(int data)
        {
            try
            {
                Menu chk = _menuRepository.GetByPredicate(x => x.Id == data);

                if (chk != null)
                {
                    chk.IsActive = false;
                    _menuRepository.Update(chk);
                    _menuRepository.SaveChanges();
                }

                return new DishoutOLOResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Category") };
            }
            catch (Exception ex)
            {
                return new DishoutOLOResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }


        public AddMenuModel GetAddMenu(int Id)
        {

            try
            {


                var menu = _menuRepository.GetListByPredicate(x => x.IsActive == true && x.Id == Id
                                     )
                                     .Select(y => new ListMenuModel()
                                     { Id = y.Id, MenuName = y.MenuName,MenuPrice=y.MenuPrice,CategoryId=y.CategoryId,Image=y.Image, IsActive = y.IsActive }
                                     ).FirstOrDefault();

                if (menu != null)
                {
                    AddMenuModel obj = new AddMenuModel();
                    obj.Id = menu.Id;
                    obj.MenuName = menu.MenuName;
                    obj.MenuPrice= menu.MenuPrice;
                    obj.IsActive = menu.IsActive;
                    obj.CategoryId = menu.CategoryId;
                    obj.Image= menu.Image; 

                    return obj;
                }
                return new AddMenuModel();
            }
            catch (Exception ex)
            {
                return new AddMenuModel();
            }

        }

        public DataTableFilterModel GetMenuList(DataTableFilterModel filter)
        {
            try
            {
                var data = _menuRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListMenuModel()
                                     { Id = y.Id, MenuName = y.MenuName,MenuPrice=y.MenuPrice,CategoryId=y.CategoryId,Image=y.Image, IsActive = y.IsActive }
                                     ).Distinct().OrderByDescending(x => x.Id).AsEnumerable();

                var sortColumn = string.Empty;
                var sortColumnDirection = string.Empty;
                if (filter.order != null && filter.order.Count() > 0)
                {
                    if (filter.order.Count() == 1)
                    {
                        sortColumnDirection = filter.order[0].dir;
                        if (filter.columns.Count() >= filter.order[0].column)
                        {
                            sortColumn = filter.columns[filter.order[0].column].data;
                        }
                    }
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                    {
                        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)) && data.Count() > 0)
                        {
                            if (sortColumn.Length > 0)
                            {
                                sortColumn = sortColumn.First().ToString().ToUpper() + sortColumn.Substring(1);
                                if (sortColumnDirection == "asc")
                                {

                                    data = data.OrderByDescending(p => p.GetType()
                                            .GetProperty(sortColumn)
                                            .GetValue(p, null)).ToList();
                                }
                                else
                                {
                                    data = data.OrderBy(p => p.GetType()
                                           .GetProperty(sortColumn)
                                           .GetValue(p, null)).ToList();
                                }
                            }
                        }
                    }
                }

                var totalCount = data.Count();
                if (!string.IsNullOrWhiteSpace(filter.search.value))
                {
                    var searchText = filter.search.value.ToLower();
                    data = data.Where(p => p.MenuName.ToLower().Contains(searchText));
                }
                var filteredCount = data.Count();
                filter.recordsTotal = totalCount;
                filter.recordsFiltered = filteredCount;
                data = data.ToList();

                filter.data = data.Skip(filter.start).Take(filter.length).ToList();

                return filter;
            }
            catch (Exception ex)
            {
                return filter;
            }

        }



    }
}
