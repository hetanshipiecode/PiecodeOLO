using DishoutOLO.Helpers.Provider;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using Microsoft.AspNetCore.Mvc;
using static DishoutOLO.ViewModel.AddCategoryModel;

namespace DishoutOLO.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private  LoggerProvider _loggerProvider;
        public CategoryController(ICategoryService categoryService, LoggerProvider loggerProvider)
        {
            _categoryService = categoryService;
            _loggerProvider = loggerProvider;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {

            return View("ManageCategory", new AddCategoryModel());
        }
        public JsonResult GetAllCategory(DataTableFilterModel filter)
        {
            try
            {
                var list = _categoryService.GetCategoryList(filter);
                return Json(list);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return Json(filter);     
        }
        public ActionResult Edit(int id)
        {
            try
            {
                _categoryService.GetCategory(id);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return View("ManageCategory");
        }


        public JsonResult AddOrUpdateCategory(AddCategoryModel categoryVM)
        {
            try
            {
                _categoryService.AddOrUpdateCategory(categoryVM);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }

            return Json(categoryVM);
        }
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var list = _categoryService.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return Json(id);
        }
    }
}
