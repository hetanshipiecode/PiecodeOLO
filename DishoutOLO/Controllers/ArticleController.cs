using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel.Helper;
using DishoutOLO.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DishoutOLO.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("ManageArticle", new AddArticleModel());
        }
        public JsonResult GetAllArticle(DataTableFilterModel filter)
        {
            var list = _articleService.GetArticleList(filter);
            return Json(list);
        }
        public ActionResult Edit(int id)
        {
            return View("ManageArticle", _articleService.GetArticle(id));
        }


        public JsonResult AddOrUpdateArticle(AddArticleModel articleVM)
        {

            return Json(_articleService.AddOrUpdateArticle(articleVM));
        }
        public IActionResult DeleteArticle(int id)
        {
            var list = _articleService.DeleteArticle(id);
            return Json(list);
        }
    }
}
