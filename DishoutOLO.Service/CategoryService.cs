using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using AutoMapper;

namespace DishoutOLO.Service
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public DishoutOLOResponseModel AddOrUpdateCategory(AddCategoryModel data)
        {
            try
            {
                var categoryresponse = _categoryRepository.GetAllAsQuerable().FirstOrDefault(x => x.IsActive == false && (x.CategoryName.ToLower() == data.CategoryName.ToLower()));
                var response = new DishoutOLOResponseModel();

                if (categoryresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (categoryresponse.CategoryName.ToLower() == data.CategoryName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "CategoryName", ErrorDescription = "Category already exist" });
                    }


                }

                if (data.Id == 0)
                {
                    Category tblCategory = _mapper.Map<AddCategoryModel, Category>(data);
                    tblCategory.CreationDate = DateTime.Now;
                    tblCategory.IsActive = true;
                    _categoryRepository.Insert(tblCategory);
                }
                else
                {
                    Category chk = _categoryRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    DateTime createdDt = chk.CreationDate; bool isActive = chk.IsActive;
                    chk = _mapper.Map<AddCategoryModel, Category>(data);
                    chk.ModifiedDate = DateTime.Now; chk.CreationDate = createdDt;chk.IsActive = isActive;
                    _categoryRepository.Update(chk);
                }
                return new DishoutOLOResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "category") : string.Format(Constants.UpdatedSuccessfully, "category") };
            }
            catch (Exception)
            {
                return new DishoutOLOResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }

        public DishoutOLOResponseModel DeleteCategory(int data)
        {
            try
            {
                Category chk = _categoryRepository.GetByPredicate(x => x.Id == data);

                if (chk != null)
                {
                    chk.IsActive = false;
                    _categoryRepository.Update(chk);
                    _categoryRepository.SaveChanges();
                }

                return new DishoutOLOResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Category") };
            }
            catch (Exception ex)
            {
                return new DishoutOLOResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }


        public DataTableFilterModel GetCategoryList(DataTableFilterModel filter)
        {
            try
            {
                var data = _categoryRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListCategoryModel()
                                     { Id = y.Id, CategoryName = y.CategoryName, IsActive = y.IsActive }
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
                                sortColumn= sortColumn.First().ToString().ToUpper() + sortColumn.Substring(1);
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
                    data = data.Where(p => p.CategoryName.ToLower().Contains(searchText));
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
                return new DishoutOLOResponseModel() { IsSuccess = true, Data = _categoryRepository.GetAll().Where(x=>x.IsActive).ToList() };
                
            }
            catch (Exception)
            {
                return new DishoutOLOResponseModel() { IsSuccess = false, Data = null };

            }
        }

        public AddCategoryModel GetAddCategory(int Id)
        {

            try
            {


                var category = _categoryRepository.GetListByPredicate(x => x.IsActive == true && x.Id == Id
                                     )
                                     .Select(y => new ListCategoryModel()
                                     { Id = y.Id, CategoryName = y.CategoryName, IsActive = y.IsActive }
                                     ).FirstOrDefault();

                if (category != null)
                {
                    AddCategoryModel obj = new AddCategoryModel();
                    obj.Id = category.Id;
                    obj.CategoryName = category.CategoryName;
                    return obj;
                }
                return new AddCategoryModel();
            }
            catch (Exception ex)
            {
                return new AddCategoryModel();
            }

        }
    }


}


