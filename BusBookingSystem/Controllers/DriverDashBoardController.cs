using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers
{
    public class DriverDashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
