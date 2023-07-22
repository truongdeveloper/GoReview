using GoReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GoReview.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BtlG21Context _context;

        public ProfileController(BtlG21Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var UserID = 1;
            var btlG21Context = _context.Posts
            .Include(p => p.User)
            .Include(p => p.Cat)
            .Include(p => p.Feedbacks);
            
            return View(await btlG21Context.ToListAsync());
        }

        public async Task<IActionResult> PrivacyAsync()
        {
            var btlG21Context = _context.Posts.Include(p => p.Cat).Include(p => p.User);
            return View(await btlG21Context.ToListAsync());
        }

    }
}