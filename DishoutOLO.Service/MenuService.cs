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
        private IRepository<Category> _categoryRepository;

        public MenuService(IRepository<Menu> menuRepository, IRepository<Category> categoryRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public DishoutOLOResponseModel AddOrUpdateMenu(AddMenuModel data)
        {
            try
            {
                var Menuresponse = _menuRepository.GetAllAsQuerable().WhereIf(data.Id > 0, x => x.Id != data.Id).FirstOrDefault(x => x.IsActive && (x.MenuName.ToLower() == data.MenuName.ToLower()));

                var response = new DishoutOLOResponseModel();

                if (Menuresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (Menuresponse.MenuName.ToLower() == data.MenuName.ToLower())
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
                    Menu chk = _menuRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    var p = chk.CreationDate;
                    chk = _mapper.Map<AddMenuModel, Menu>(data);
                    chk.CreationDate = p;
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
                                     {
                                         Id = y.Id,
                                         MenuName = y.MenuName,
                                         MenuPrice = y.MenuPrice,
                                         CategoryId = y.CategoryId,
                                         Image = y.Image,
                                         IsActive = y.IsActive,
                                         CategoryName = y.CategoryName
                                     }
                                     ).FirstOrDefault();


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
                                                return new AddMenuModel();
                                            }

        }
        public DataTableFilterModel GetMenuList(DataTableFilterModel filter)
        {
            try
            {
                //var data = _menuRepository.GetListByPredicate(x => x.IsActive == true
                //                     )
                //                     .Select(y => new ListMenuModel()
                //                     { Id = y.Id, MenuName = y.MenuName, MenuPrice = y.MenuPrice, CategoryId = y.CategoryId, Image = y.Image, IsActive = y.IsActive }
                //                     ).Distinct().OrderByDescending(x => x.Id).AsEnumerable();

                var data = (from ct in _categoryRepository.GetAll()
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



    }
}
