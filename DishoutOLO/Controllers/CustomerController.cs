using DishoutOLO.Helpers.Provider;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel.Helper;
using Microsoft.AspNetCore.Mvc;

namespace DishoutOLO.Controllers
{
    public class CustomerController : Controller
    {
        #region Declarations
        private readonly ICustomerService _customerService;
        private LoggerProvider _loggerProvider;
        #endregion

        #region Constructor
        public CustomerController(ICustomerService customerService, LoggerProvider loggerProvider)
        {
            _customerService = customerService;
            _loggerProvider = loggerProvider;
        }
        #endregion

     
        #region Get Methods
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get All Customer List
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public JsonResult GetAllCustomer(DataTableFilterModel filter)
        {
            try
            {
                DataTableFilterModel list = _customerService.GetCustomerList(filter);
                return Json(list);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return Json(filter);
        }
        
        #endregion
    }
}
