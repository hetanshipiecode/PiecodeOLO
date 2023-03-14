using AutoMapper;
using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;

namespace DishoutOLO.Service
{

    public class ItemService : IitemService
    {

        #region Declarations
        private IRepository<Item> _itemRepository;
        private IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        #endregion


        #region Constructor
        public ItemService(IRepository<Item> itemRepository, IMapper mapper, IRepository<Category> categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;

            _mapper = mapper;
        }


        #endregion
        #region Crud Methods

        public DishoutOLOResponseModel AddOrUpdateItem(AddItemModel data)
        {
            try
            {
                Item Item = _itemRepository.GetAllAsQuerable().FirstOrDefault(x => x.IsActive == false && (x.ItemName.ToLower() == data.ItemName.ToLower()));
                DishoutOLOResponseModel response = new DishoutOLOResponseModel();

                if (Item != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (Item.ItemName.ToLower() == data.ItemName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "ItemName", ErrorDescription = "Item already exist" });
                    }

                }
                if ( response.Errors==null)
                {
                    if (data.Id == 0)
                    {

                        Item tblItem = _mapper.Map<AddItemModel, Item>(data);
                        tblItem.CreationDate = DateTime.Now;
                        tblItem.IsActive = true;
                        _itemRepository.Insert(tblItem);
                    }
                    else
                    {
                        Item item = _itemRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                        DateTime createdDt = item.CreationDate;
                        bool isActive = item.IsActive;
                        item = _mapper.Map<AddItemModel, Item>(data);
                        item.ModifiedDate = DateTime.Now; item.CreationDate = createdDt; item.IsActive = isActive;
                        _itemRepository.Update(item);
                    }
                }
                else
                {
                    return response;
                }

                return new DishoutOLOResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "Item") : string.Format(Constants.UpdatedSuccessfully, "Item") };
            }
            catch (Exception)
            {
                return new DishoutOLOResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }

        public DishoutOLOResponseModel DeleteItem(int data)
        {
            try
            {
                Item item = _itemRepository.GetByPredicate(x => x.Id == data);
                    
                if (item != null)
                {
                    item.IsActive = false;
                    _itemRepository.Update(item);
                    _itemRepository.SaveChanges();
                }

                return new DishoutOLOResponseModel { IsSuccess = true, Data = item.ItemImage, Message = string.Format(Constants.DeletedSuccessfully, "Item") };
            }
            catch (Exception ex)
            {
                return new DishoutOLOResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }
        #endregion


        #region Get Methods
        public DataTableFilterModel GetItemList(DataTableFilterModel filter)
        {
            try
            {
                IEnumerable<ListItemModel> data = (from ct in _categoryRepository.GetAll()
                                                   join it in _itemRepository.GetAll() on
                                                   ct.Id equals it.CategoryId
                                                   where it.IsActive =true
                                                  
                                                   select new ListItemModel
                                                   {
                                                       CategoryName = ct.CategoryName,
                                                       ItemName = it.ItemName,
                                                       ItemImage = it.ItemImage,
                                                       IsCombo = it.IsCombo,
                                                       IsActive = it.IsActive,
                                                       Id = it.Id,

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
                    data = data.Where(p => p.ItemName.ToLower().Contains(searchText));
                }
                var filteredCount = data.Count();
                filter.recordsTotal = totalCount;
                filter.recordsFiltered = filteredCount;
                if (!string.IsNullOrEmpty(filter.CategoryName))
                {
                    data = data.Where(x => x.CategoryName == filter.CategoryName).ToList();
                }
                if (!string.IsNullOrEmpty(filter.ItemName))
                {
                    data = data.Where(x => x.ItemName == filter.ItemName).ToList();
                }
                if (string.IsNullOrEmpty(filter.ItemName) && string.IsNullOrEmpty(filter.CategoryName))
                    data = data.ToList();

                filter.data = data.Skip(filter.start).Take(filter.length).ToList();

                return filter;
            }
            catch (Exception ex)
            {
                return filter;
            }

        }

        public DishoutOLOResponseModel GetAllCategories()
        {
            try
            {
                return new DishoutOLOResponseModel() { IsSuccess = true, Data = _itemRepository.GetAll().Where(x => x.IsCombo).ToList() };

            }
            catch (Exception)
            {
                return new DishoutOLOResponseModel() { IsSuccess = false, Data = null };

            }
        }


        public AddItemModel GetItem(int Id)
        {
            try
            {
               ListItemModel item = _itemRepository.GetListByPredicate(x => x.IsActive  && x.Id == Id
                                     )
                                     .Select(y => new ListItemModel()
                                     { Id = y.Id, ItemName = y.ItemName, IsCombo = y.IsCombo, IsTax = y.IsTax, IsVeg = y.IsVeg, IsActive = y.IsActive, CategoryId = y.CategoryId, ItemDescription = y.ItemDescription }
                                     ).FirstOrDefault();

                if (item != null)
                {
                    AddItemModel obj = new AddItemModel();
                    obj.Id = item.Id;
                    obj.ItemName = item.ItemName;
                    obj.IsVeg = item.IsVeg;
                    obj.IsTax = item.IsTax;
                    obj.IsCombo = item.IsCombo;
                    obj.ItemDescription = item.ItemDescription;
                    obj.CategoryId = item.CategoryId;

                    return obj;
                }
                return new AddItemModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }

}
