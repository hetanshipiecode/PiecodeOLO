using DishoutOLO.Data;
using DishoutOLO.Repo;
using DishoutOLO.Service;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using Microsoft.AspNetCore.Hosting.Server;
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


        public JsonResult AddOrUpdateMenu(AddMenuModel menuVM,IFormFile file)
        {
            AddMenuModel menuModel = new AddMenuModel();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Menu");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //get file extension
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileName = file.FileName + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            menuVM.Image = "~/Content/Menu" + "/" + fileName;
            var images = Directory.GetDirectories("Path")
                             .Select(fn => "Path" + Path.GetFileName(fn));
           return Json(_menuService.AddOrUpdateMenu(menuVM));
        }
        public IActionResult DeleteMenu(int id)
        {
            var list = _menuService.DeleteMenu(id);
            return Json(list);
        }


    }
}
