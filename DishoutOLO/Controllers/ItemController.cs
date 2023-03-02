using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel.Helper;
using DishoutOLO.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using DishoutOLO.Service;
using DishoutOLO.Helpers;

namespace DishoutOLO.Controllers
{
    public class ItemController : Controller
    {
        private readonly IitemService _ItemService;

        public ItemController(IitemService itemService)
        {
            _ItemService = itemService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View("ManageItem",new AddItemModel());
        }

        public JsonResult GetAllItem(DataTableFilterModel filter)
        {
            var list = _ItemService.GetItemList(filter);
            return Json(list);
        }
        public ActionResult Edit(int id)
        {
            return View("ManageCategory", _ItemService.GetAddItem(id));
        }


        public JsonResult AddOrUpdateItem(AddItemModel itemVM,IFormFile file)
        {
            if (file != null)
            {
                string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Item", fileName);
                Utility.SaveFile(file, path);
                if (itemVM.Id > 0)
                {
                    Utility.DeleteFile(path);
                }
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
