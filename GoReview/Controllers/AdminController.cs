using Microsoft.AspNetCore.Mvc;

namespace GoReview.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
