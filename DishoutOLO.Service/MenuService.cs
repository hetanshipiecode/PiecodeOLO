using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using AutoMapper;
namespace DishoutOLO.Service
{
    public class MenuService : IMenuService
    {
        #region Declarations
        private readonly IMapper _mapper;
        private IRepository<Menu> _menuRepository;
        private IRepository<Category> _categoryRepository;

        #endregion
        #region Constructor
        public MenuService(IRepository<Menu> menuRepository, IRepository<Category> categoryRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        #endregion

        #region Crud Methods
        public DishoutOLOResponseModel AddOrUpdateMenu(AddMenuModel data, string imgPath = "")
        {
            try
            {
                Menu Menu = _menuRepository.GetAllAsQuerable().WhereIf(data.Id > 0, x => x.Id != data.Id).FirstOrDefault(x => x.IsActive && (x.MenuName.ToLower() == data.MenuName.ToLower()));

                DishoutOLOResponseModel response = new DishoutOLOResponseModel();

                if (Menu != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (Menu.MenuName.ToLower() == data.MenuName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "MenuName", ErrorDescription = "Menu already exist" });
                    }

                }
                if (data.Id == 0)
                {
                    Menu tblMenu = _mapper.Map<AddMenuModel, Menu>(data);
                    tblMenu.CreationDate = DateTime.Now;
                    tblMenu.IsActive = true;
                    _menuRepository.Insert(tblMenu);
                }
                else
                {
                    Menu menu = _menuRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    DateTime CreationDate = menu.CreationDate;
                    menu = _mapper.Map<AddMenuModel, Menu>(data);
                    menu.CreationDate = CreationDate;
                    menu.ModifiedDate = DateTime.Now;
                    menu.Image = imgPath;
                    _menuRepository.Update(menu);
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
                Menu menu = _menuRepository.GetByPredicate(x => x.Id == data);

                if (menu != null)
                {
                    menu.IsActive = false;
                    _menuRepository.Update(menu);
                    _menuRepository.SaveChanges();
                }

                return new DishoutOLOResponseModel { IsSuccess = true, Data = menu.Image, Message = string.Format(Constants.DeletedSuccessfully, "Menu") };
            }
            catch (Exception ex)
            {
                return new DishoutOLOResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }
        #endregion

        #region  Get methods
        public AddMenuModel GetMenu(int Id)
        {
            try
            {
               ListMenuModel menu = _menuRepository.GetListByPredicate(x => x.IsActive == true && x.Id == Id).Select(y => new ListMenuModel()
                {
                Id = y.Id,
                MenuName = y.MenuName,
                MenuPrice = y.MenuPrice,
                CategoryId = y.CategoryId,
                Image = y.Image,
                IsActive = y.IsActive,
                CategoryName = y.CategoryName
                }).FirstOrDefault();
                
                if (menu != null)
                {
                    AddMenuModel obj = new AddMenuModel();
                    obj.Id = menu.Id;
                    obj.MenuName = menu.MenuName;
                    obj.MenuPrice = menu.MenuPrice;
                    obj.IsActive = menu.IsActive;
                    obj.CategoryId = menu.CategoryId;
                    obj.Image = menu.Image;

                    return obj;
                }
                return new AddMenuModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTableFilterModel GetMenuList(DataTableFilterModel filter)
        {
            try
            {

                IEnumerable<ListMenuModel> data = (from ct in _categoryRepository.GetAll()
                            join mn in _menuRepository.GetAll() on
                            ct.Id equals mn.CategoryId
                            where mn.IsActive == true
                            select new ListMenuModel

                            {
                                CategoryName = ct.CategoryName,
                                MenuName = mn.MenuName,
                                MenuPrice = mn.MenuPrice,
                                Image = mn.Image,
                                Id = mn.Id,
                            }).AsEnumerable();


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
      

        #endregion


    }
}
