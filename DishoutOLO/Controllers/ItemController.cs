using Microsoft.AspNetCore.Mvc;

namespace DishoutOLO.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Item()
        {
            return View();
        }
    }
}
