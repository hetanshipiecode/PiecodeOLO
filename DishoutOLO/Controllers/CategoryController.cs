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
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
             var list = _categoryService.GetCategoryList(filter);
             return Json(list);     
        }
        public ActionResult Edit(int id)
        {   
            return View("ManageCategory", _categoryService.GetCategory(id));
        }


        public JsonResult AddOrUpdateCategory(AddCategoryModel categoryVM)
        {
               
              return Json(_categoryService.AddOrUpdateCategory(categoryVM));
        }
        public IActionResult DeleteCategory(int id)
        {
            var list = _categoryService.DeleteCategory(id);
            return Json(list);
        }
    }
}
