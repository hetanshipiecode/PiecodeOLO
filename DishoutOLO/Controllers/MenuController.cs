using DishoutOLO.Repo;
using DishoutOLO.Service;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using static DishoutOLO.ViewModel.AddMenuModel;

namespace DishoutOLO.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService  _menuService;
        private readonly ICategoryService _categoryService;

        public MenuController(IMenuService menuService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _menuService = menuService;
        }
        public IActionResult Index()
        {


            return View();
        }
       
        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList((IList)_categoryService.GetAllCategories().Data, "Id", "CategoryName");

            return View("ManageMenu", new AddMenuModel());  
        }

        public JsonResult GetAllMenu(DataTableFilterModel filter)
        {
            var list = _menuService.GetMenuList(filter);
            return Json(list);
        }
        public ActionResult Edit(int id)
        {
            return View("ManageMenu", _menuService.GetAddMenu(id));
        }


        public JsonResult AddOrUpdateMenu(AddMenuModel menuVM)
        {
            AddMenuModel menuModel = new AddMenuModel();
         

            return Json(_menuService.AddOrUpdateMenu(menuVM));
        }
        public IActionResult DeleteMenu(int id)
        {
            var list = _menuService.DeleteMenu(id);
            return Json(list);
        }


    }
}
