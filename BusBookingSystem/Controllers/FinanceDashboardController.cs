using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers
{
    public class FinanceDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
