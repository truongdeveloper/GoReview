using GoReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Security.Claims;

namespace GoReview.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BtlG21Context _context;

        public HomeController(ILogger<HomeController> logger, BtlG21Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var _post = _context.Posts.Include(p => p.Cat).Include(p => p.User).Include(p => p.Feedbacks);

            if(User.Identity.IsAuthenticated)
            {
                int loggedInUserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                foreach (var post in _post)
                {
                    // Kiểm tra xem người dùng đã like bài viết hay chưa
                    post.IsLiked = post.Feedbacks.Any(f => f.UserId == loggedInUserId && f.Like == true);
                }
            }


            return View(_post);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string keyword)
        {
            var _post = _context.Posts.Include(p => p.Cat).Include(p => p.User).Include(p => p.Feedbacks);

            if (User.Identity.IsAuthenticated)
            {
                int loggedInUserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                foreach (var post in _post)
                {
                    // Kiểm tra xem người dùng đã like bài viết hay chưa
                    post.IsLiked = post.Feedbacks.Any(f => f.User.UserId == loggedInUserId && f.Like == true);
                }
            }

            List<Post> postFillter = _post.Where(p => p.Title.Contains(keyword)).ToList();


            return Json(new
            {
                Title = "truong",
                Content = "truong"
            });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}