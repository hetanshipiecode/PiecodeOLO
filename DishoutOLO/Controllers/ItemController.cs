using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel.Helper;
using DishoutOLO.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using DishoutOLO.Service;
using DishoutOLO.Helpers;
using DishoutOLO.Data;

namespace DishoutOLO.Controllers
{
    public class ItemController : Controller
    {
        private readonly IitemService _ItemService;
        private readonly ICategoryService _categoryService;

        public ItemController(IitemService itemService, ICategoryService categoryService)
        {
            _ItemService = itemService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            ViewBag.CategoryList = new SelectList((IList)_categoryService.GetAllCategories().Data, "Id", "CategoryName");
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList= new SelectList((IList)_categoryService.GetAllCategories().Data, "Id", "CategoryName");
            return View("ManageItem",new AddItemModel());
        }

        public JsonResult GetAllItem(DataTableFilterModel filter)
        {           
            
            var CategoryName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                    var ItemName = Request.Form["columns[2][search][value]"].FirstOrDefault();
            filter.CategoryName = CategoryName;
            filter.ItemName = ItemName;
            var list = _ItemService.GetItemList(filter);
            return Json(list);
        }
        public ActionResult Edit(int id )
        {
            //if (file != null)
            //{
            //    string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
            //    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Item", fileName);
            //    Utility.SaveFile(file, path);
            //    if (itemVM.Id > 0)
            //    {
            //        Utility.DeleteFile(path);
            //    }
            //    itemVM.ItemImage = fileName;
            //}
            ViewBag.CategoryList = new SelectList((IList)_categoryService.GetAllCategories().Data, "Id", "CategoryName");
            var f = _ItemService.GetItem(id);
            return View("ManageItem", _ItemService.GetItem(id));
        }


       
        public JsonResult AddOrUpdateItem(AddItemModel itemVM,IFormFile file)
        {
            AddItemModel itemModel = new AddItemModel();

            if (file != null)
            {
                    string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Item", fileName);
                Utility.SaveFile(file, path);
                //if (itemVM.Id > 0)
                //{
                //    Utility.DeleteFile(path);
                //}
                itemVM.ItemImage = fileName;
            }
            return Json(_ItemService.AddOrUpdateItem(itemVM));

        }
        public IActionResult DeleteItem(int id)
        {
            var list = _ItemService.DeleteItem(id);
            return Json(list);
        }




    }
}
