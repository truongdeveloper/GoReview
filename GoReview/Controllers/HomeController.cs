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

        public async Task<IActionResult> Index(string keyword)
        {
            IQueryable<Post> query = _context.Posts.Include(p => p.Cat).Include(p => p.User).Include(p => p.Feedbacks);

            if (User.Identity.IsAuthenticated)
            {
                int loggedInUserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                foreach (var post in query)
                {
                    // Kiểm tra xem người dùng đã like bài viết hay chưa
                    post.IsLiked = _context.Feedbacks.Any(f => f.UserId == loggedInUserId && f.Like == true);
                }
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                // Nếu có từ khóa tìm kiếm, thực hiện lọc bài viết theo từ khóa
                query = query.Where(p => p.Title.Contains(keyword));
                var postsSearch = await query.ToListAsync();
                return PartialView(postsSearch);
            }

            var posts = await query.ToListAsync();
            // Thêm các xử lý cần thiết, ví dụ như tính toán số lượng lượt thích, số lượng bình luận, ...
            return View(posts);
        }

        //[HttpPost]
        //public async Task<IActionResult> IndexAsync(string keyword)
        //{
        //    var _post = _context.Posts.Include(p => p.Cat).Include(p => p.User).Include(p => p.Feedbacks);

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        int loggedInUserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        //        foreach (var post in _post)
        //        {
        //            // Kiểm tra xem người dùng đã like bài viết hay chưa
        //            post.IsLiked = post.Feedbacks.Any(f => f.User.UserId == loggedInUserId && f.Like == true);
        //        }
        //    }

        //    List<Post> postFillter = _post.Where(p => p.Title.Contains(keyword)).ToList();


        //    return Json(new
        //    {
        //        Title = "truong",
        //        Content = "truong"
        //    });
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}