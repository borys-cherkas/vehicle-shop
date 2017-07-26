using Microsoft.AspNetCore.Mvc;

namespace VehicleShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            ViewBag.Message = "Some Error Message";
            return View();
        }
    }
}
