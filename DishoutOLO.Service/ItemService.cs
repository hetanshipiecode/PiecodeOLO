using AutoMapper;
using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;


namespace DishoutOLO.Service
{
    
    public class ItemService: IitemService
    {
        private IRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IRepository<Item> itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }


        public DishoutOLOResponseModel AddOrUpdateItem(AddItemModel data)
        {
            try
            {
                var itemresponse = _itemRepository.GetAllAsQuerable().FirstOrDefault(x => x.IsActive == false && (x.ItemName.ToLower() == data.ItemName.ToLower()));
                var response = new DishoutOLOResponseModel();

                if (itemresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (itemresponse.ItemName.ToLower() == data.ItemName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "ItemName", ErrorDescription = "Item already exist" });
                    }

                }
                if (data.Id == 0)
                {
                    Item tblItem = _mapper.Map<AddItemModel, Item>(data);
                    tblItem.CreationDate = DateTime.Now;
                    tblItem.IsActive = true;
                    _itemRepository.Insert(tblItem);
                }
                else
                {
                    Item chk = _itemRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    DateTime createdDt = chk.CreationDate; bool isActive = chk.IsActive;
                    chk = _mapper.Map<AddItemModel, Item>(data);
                    chk.ModifiedDate = DateTime.Now; chk.CreationDate = createdDt; chk.IsActive = isActive;
                    _itemRepository.Update(chk);
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
                Item chk = _itemRepository.GetByPredicate(x => x.Id == data);

                if (chk != null)
                {
                    chk.IsActive = false;
                    _itemRepository.Update(chk);
                    _itemRepository.SaveChanges();
                }

                return new DishoutOLOResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Item") };
            }
            catch (Exception ex)
            {
                return new DishoutOLOResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }

        public DataTableFilterModel GetItemList(DataTableFilterModel filter)
        {
            try
            {
                var data = _itemRepository.GetListByPredicate(x => x.IsCombo == true
                                     )
                                     .Select(y => new ListItmeModel()
                                     { Id = y.Id, ItemName = y.ItemName, IsCombo = y.IsCombo }
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
                    data = data.Where(p => p.ItemName.ToLower().Contains(searchText));
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


        public AddItemModel GetAddItem(int Id)
        {
            try
            {
                var item = _itemRepository.GetListByPredicate(x => x.IsCombo == true && x.Id == Id
                                     )
                                     .Select(y => new ListItmeModel()
                                     { Id = y.Id, ItemName = y.ItemName, IsCombo = y.IsCombo }
                                     ).FirstOrDefault();

                if (item != null)
                {
                    AddItemModel obj = new AddItemModel();
                    obj.Id = item.Id;
                    obj.ItemName = item.ItemName;
                    return obj;
                }
                return new AddItemModel();
            }
            catch (Exception ex)
            {
                return new AddItemModel();
            }

        }
    }

}
