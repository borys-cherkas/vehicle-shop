using Microsoft.AspNetCore.Mvc;

namespace VehicleShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
