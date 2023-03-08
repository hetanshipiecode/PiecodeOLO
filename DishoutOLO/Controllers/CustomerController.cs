using DishoutOLO.Helpers.Provider;
using DishoutOLO.Service.Interface;
using DishoutOLO.ViewModel.Helper;
using Microsoft.AspNetCore.Mvc;

namespace DishoutOLO.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private LoggerProvider _loggerProvider;

        public CustomerController(ICustomerService customerService, LoggerProvider loggerProvider)
        {
            _customerService = customerService;
            _loggerProvider = loggerProvider;
        }
        public IActionResult Index()
        {
            return View();
        }


        public JsonResult GetAllCustomer(DataTableFilterModel filter)
        {
            try
            {
                var list = _customerService.GetCustomerList(filter);
                return Json(list);
            }
            catch (Exception ex)
            {
                _loggerProvider.logmsg(ex.Message);
            }
            return Json(filter);
        }
    }
}
