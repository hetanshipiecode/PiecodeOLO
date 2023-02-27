using DishoutOLO.Data;
using DishoutOLO.Repo;
using DishoutOLO.Repo.Interface;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using static DishoutOLO.ViewModel.DishoutOLOResponseModel;
using Constants = DishoutOLO.ViewModel.Helper.Constants;

namespace DishoutOLO.Service
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public DishoutOLOResponseModel AddOrUpdateCategory(CategoryModel data)
        {
            try
            {
                var categoryresponse = _categoryRepository.GetAllAsQuerable().FirstOrDefault(x => x.IsActive==false && (x.CategoryName.ToLower() == data.CategoryName.ToLower()));
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
                    Category tblCategory = new Category
                    {
                        CategoryName = data.CategoryName,


                        IsActive = true,
                    };
                    _categoryRepository.Insert(tblCategory);
                }
                else
                {
                    Category chk = _categoryRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    chk.Id = data.Id;
                    chk.CategoryName = data.CategoryName;

                }
                return new DishoutOLOResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "category") : string.Format(Constants.UpdatedSuccessfully, "category") };
            }
            catch (Exception ex)
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
    

