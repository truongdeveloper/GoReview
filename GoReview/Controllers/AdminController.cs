using GoReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoReview.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly BtlG21Context _context;

        public AdminController(BtlG21Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var UserID = 1;
            var user = _context.Users.FirstOrDefault( p => p.UserId == UserID);
            return View(user);
        }
    }
}
