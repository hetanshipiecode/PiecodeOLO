using AutoMapper;
using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel.Helper;
using DishoutOLO.ViewModel;
namespace DishoutOLO.Service
{
    public class ArticleService : IArticleService
    {
        #region Declarations
        private IRepository<Article> _articleRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public ArticleService(IRepository<Article> articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }
        #endregion

        #region Crud Methods
        public DishoutOLOResponseModel AddOrUpdateArticle(AddArticleModel data)
        {
            try
            {
                Article Article = _articleRepository.GetAllAsQuerable().FirstOrDefault(x => x.IsActive == false && (x.ArticleName.ToLower() == data.ArticleName.ToLower()));
                DishoutOLOResponseModel response = new DishoutOLOResponseModel();

                if (Article != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (Article.ArticleName.ToLower() == data.ArticleName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "ArticleName", ErrorDescription = "Article already exist" });
                    }

                }
                if (data.Id == 0)
                {
                    Article tblArticle = _mapper.Map<AddArticleModel, Article>(data);
                    tblArticle.CreationDate = DateTime.Now;
                    tblArticle.IsActive = true;
                    _articleRepository.Insert(tblArticle);
                }
                else
                {
                    Article article = _articleRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    DateTime createdDt = article.CreationDate; bool isActive = article.IsActive;
                    article = _mapper.Map<AddArticleModel, Article>(data);
                    article.ModifiedDate = DateTime.Now; article.CreationDate = createdDt; article.IsActive = isActive;
                    _articleRepository.Update(article);
                }
                return new DishoutOLOResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "article") : string.Format(Constants.UpdatedSuccessfully, "article") };
            }
            catch (Exception)
            {
                return new DishoutOLOResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }

        public DishoutOLOResponseModel DeleteArticle(int data)
        {
            try
            {
                Article chk = _articleRepository.GetByPredicate(x => x.Id == data);

                if (chk != null)
                {
                    chk.IsActive = false;
                    _articleRepository.Update(chk);
                    _articleRepository.SaveChanges();
                }

                return new DishoutOLOResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Article") };
            }
            catch (Exception ex)
            {
                return new DishoutOLOResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }

        #endregion
        #region Get Methods
        public DataTableFilterModel GetArticleList(DataTableFilterModel filter)
        {
            try
            {
                IEnumerable<ListArticleModel> data = _articleRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListArticleModel()
                                     { Id = y.Id, ArticleName = y.ArticleName, ArticleDescription = y.ArticleDescription, IsActive = y.IsActive }
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
                    data = data.Where(p => p.ArticleName.ToLower().Contains(searchText));
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

        public AddArticleModel GetArticle(int Id)
        {
            try
            {
                ListArticleModel article = _articleRepository.GetListByPredicate(x => x.IsActive == true && x.Id == Id
                                     )
                                     .Select(y => new ListArticleModel()
                                     { Id = y.Id, ArticleName = y.ArticleName, ArticleDescription = y.ArticleDescription, IsActive = y.IsActive }
                                     ).FirstOrDefault();

                if (article != null)
                {
                    AddArticleModel obj = new AddArticleModel();
                    obj.Id = article.Id;
                    obj.ArticleName = article.ArticleName;
                    obj.ArticleDescription = article.ArticleDescription;
                    return obj;
                }
                return new AddArticleModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        } 
        #endregion
    }
}
