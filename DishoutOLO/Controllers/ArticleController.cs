using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel.Helper;
using DishoutOLO.ViewModel;
using Microsoft.AspNetCore.Mvc;
using DishoutOLO.Data;
using DishoutOLO.Helpers.Provider;

namespace DishoutOLO.Controllers
{
    public class ArticleController : Controller
    {
        #region Declarations
        private readonly IArticleService _articleService;
        private LoggerProvider _loggerProvider;
        #endregion
        #region Constructor
        public ArticleController(IArticleService articleService,LoggerProvider loggerProvider)
        {
            _articleService = articleService;
            _loggerProvider=loggerProvider;
        }
        #endregion
        #region Get Methods
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Create Article
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View("ManageArticle", new AddArticleModel());
        }
        /// <summary>
        /// Get All Article List
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public JsonResult GetAllArticle(DataTableFilterModel filter)
        {
            try
            {
                var list = _articleService.GetArticleList(filter);
                return Json(list);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return Json(filter);
        }
        /// <summary>
        /// go to edit page with update data 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            try
            {
                _articleService.GetArticle(id);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return View("ManageArticle");
        }
        #endregion


        #region Crud Methods
        /// <summary>
        ///  To add or insert Article
        /// </summary>
        /// <param name="articleVM"></param>
        /// <returns></returns>
        public JsonResult AddOrUpdateArticle(AddArticleModel articleVM)
        {
            try
            {
                _articleService.AddOrUpdateArticle(articleVM);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);

            }
            return Json(articleVM);
        }
        /// <summary>
        /// Delete Article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteArticle(int id)
        {
            try
            {
                DishoutOLOResponseModel list = _articleService.DeleteArticle(id);
                return Json(list);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);

            }
            return Json(id);
        } 
        #endregion
    }
}
