using DishoutOLO.Helpers;
using DishoutOLO.Repo;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace DishoutOLO.Controllers
{
    
    public class MenuController : Controller
    {
        

        private readonly IMenuService  _menuService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MenuController(IMenuService menuService, ICategoryService categoryService, IWebHostEnvironment hostingEnvironment)
        {
            _categoryService = categoryService;
            _menuService = menuService;
            _hostingEnvironment = hostingEnvironment;
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
        public ActionResult Edit(int id,IFormFile file)
        {
            ViewBag.CategoryList = new SelectList((IList)_categoryService.GetAllCategories().Data, "Id", "CategoryName");
            return View("ManageMenu", _menuService.GetAddMenu(id));
        }
        
        public JsonResult AddOrUpdateMenu(AddMenuModel menuVM,IFormFile file)
        {
            if (file != null)
            {
                string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
                string path =  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Menu",fileName);
                Utility.SaveFile(file, path);
                Utility.DeleteFile(path);

                menuVM.Image = fileName;
            }
            return Json(_menuService.AddOrUpdateMenu(menuVM));
        }
        public IActionResult DeleteMenu(int id)
        {
            var list = _menuService.DeleteMenu(id);
            if(list!=null && list.Data != null)
            {
                var path = (string)list.Data;
                if (!string.IsNullOrEmpty(path))
                {
                    Utility.DeleteFile(path);
                }
            }
            return Json(list);
        }


    }
}
