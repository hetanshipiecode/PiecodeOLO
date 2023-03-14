﻿using DishoutOLO.Helpers.Provider;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel;
using DishoutOLO.ViewModel.Helper;
using Microsoft.AspNetCore.Mvc;
using static DishoutOLO.ViewModel.AddCategoryModel;

namespace DishoutOLO.Controllers
{
    public class CategoryController : Controller
    {
        #region Declarations

        private readonly ICategoryService _categoryService;
        private LoggerProvider _loggerProvider;

        #endregion
        #region Constructor
        public CategoryController(ICategoryService categoryService, LoggerProvider loggerProvider)
        {
            _categoryService = categoryService;
            _loggerProvider = loggerProvider;
        }


        #endregion

        #region Crud Methods
        public IActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {

            return View("ManageCategory", new AddCategoryModel());
        }
        /// <summary>
        /// To add or insert category
        /// </summary>
        /// <param name="categoryVM"></param>
        /// <returns></returns>
        public JsonResult AddOrUpdateCategory(AddCategoryModel categoryVM)
        {
            try
            {
                return Json(_categoryService.AddOrUpdateCategory(categoryVM));
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }

            return Json(categoryVM);
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                DishoutOLOResponseModel list = _categoryService.DeleteCategory(id);
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
        /// Get All Category List
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public JsonResult GetAllCategory(DataTableFilterModel filter)
        {
            try
            {
                DataTableFilterModel list = _categoryService.GetCategoryList(filter);
                return Json(list);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return Json(filter);
        }
        /// <summary>
        ///  go to edit page with update data 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return View("ManageCategory", _categoryService.GetCategory(id));
        }

        #endregion
    }
}
