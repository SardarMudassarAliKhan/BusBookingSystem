using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers
{
    public class OperationDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
