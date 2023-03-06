using DishoutOLO.Helpers;
using DishoutOLO.Helpers.Provider;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace DishoutOLO.Controllers
{

    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger _logger;
        private LoggerProvider _loggerProvider;

        public MenuController(IMenuService menuService, ICategoryService categoryService, IWebHostEnvironment hostingEnvironment,LoggerProvider loggerProvider)
        {
            _categoryService = categoryService;
            _menuService = menuService;
            _hostingEnvironment = hostingEnvironment;
            _loggerProvider= loggerProvider;    
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            try
            {
                ViewBag.CategoryList = new SelectList((IList)_categoryService.GetAllCategories().Data, "Id", "CategoryName");

            }
            catch (Exception ex) 
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return View("ManageMenu", new AddMenuModel());
        }

        public JsonResult GetAllMenu(DataTableFilterModel filter)
        {
            try
            {
                var list = _menuService.GetMenuList(filter);
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
                ViewBag.CategoryList = new SelectList((IList)_categoryService.GetAllCategories().Data, "Id", "CategoryName");

            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return View("ManageMenu", _menuService.GetMenu(id));
        }


        public JsonResult AddOrUpdateMenu(AddMenuModel menuVM, IFormFile file)
        {
            try
            {
                AddMenuModel menuModel = new AddMenuModel();
                if (file != null)
                {
                    string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Menu", fileName);
                    Utility.SaveFile(file, path);

                    menuVM.Image = fileName;
                }
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return Json(_menuService.AddOrUpdateMenu(menuVM,menuVM.Id>0?menuVM.Image:string.Empty));
        }
        public IActionResult DeleteMenu(int id)
        {
            try
            {
                var list = _menuService.DeleteMenu(id);
                return Json(list);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }

            return Json(id);
        }


    }
}
