using Microsoft.AspNetCore.Mvc;

namespace bluemarket.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}