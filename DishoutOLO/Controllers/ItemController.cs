using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel.Helper;
using DishoutOLO.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using DishoutOLO.Helpers;
using DishoutOLO.Data;
using Serilog;
using DishoutOLO.Helpers.Provider;

namespace DishoutOLO.Controllers
{
    public class ItemController : Controller
    {
        #region Declarations
        private readonly IitemService _ItemService;
        private readonly ICategoryService _categoryService;
        private LoggerProvider _loggerProvider;

        #endregion

        #region Constructor
        public ItemController(IitemService itemService, ICategoryService categoryService, LoggerProvider loggerProvider)
        {
            _ItemService = itemService;
            _categoryService = categoryService;
            _loggerProvider= loggerProvider;
        }
        #endregion

        #region Crud Methods
        public IActionResult Index()
        {
            try
            {
                _loggerProvider.logmsg("Okak");
                ViewBag.CategoryList = new SelectList((IList)_categoryService.GetAllCategories().Data, "Id", "CategoryName");

            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);

            }
            return View();
        }
        /// <summary>
        /// Create Item
        /// </summary>
        /// <returns></returns>
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
            return View("ManageItem", new AddItemModel());
        }

        /// <summary>
        /// To add or insert Item
        /// </summary>
        /// <param name="itemVM"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public JsonResult AddOrUpdateItem(AddItemModel itemVM, IFormFile file)
        {
            try
            {
                AddItemModel itemModel = new AddItemModel();

                if (file != null)
                {
                    string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Item", fileName);
                    Utility.SaveFile(file, path);
                    itemVM.ItemImage = fileName;
                }
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);

            }
            return Json(_ItemService.AddOrUpdateItem(itemVM));

        }
        /// <summary>
        /// Delete Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteItem(int id)
        {
            try
            {
                DishoutOLOResponseModel list = _ItemService.DeleteItem(id);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);

            }
            return Json(id);
        }
        #endregion

        #region Get Methods
        /// <summary>
        /// Get All Item List
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public JsonResult GetAllItem(DataTableFilterModel filter)
        {
            try
            {
                var CategoryName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var ItemName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                filter.CategoryName = CategoryName;
                filter.ItemName = ItemName;
                DataTableFilterModel list = _ItemService.GetItemList(filter);
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
                ViewBag.CategoryList = new SelectList((IList)_categoryService.GetAllCategories().Data, "Id", "CategoryName", id);

            }

            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);

            }
            return View("ManageItem", _ItemService.GetItem(id));

        }
       

        #endregion
    }
}
