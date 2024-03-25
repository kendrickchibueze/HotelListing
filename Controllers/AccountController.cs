using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
