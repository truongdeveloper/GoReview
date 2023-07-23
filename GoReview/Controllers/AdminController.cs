using GoReview.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GoReview.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly BtlG21Context _context;

        public AdminController(BtlG21Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
    }
}
