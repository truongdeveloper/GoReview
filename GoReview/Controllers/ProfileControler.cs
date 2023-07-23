using GoReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

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

            if(User.Identity.IsAuthenticated)
            {
                var UserID = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var _post = _context.Posts
                    .Include(p => p.Cat)
                    .Include(p => p.User)
                    .Include(p => p.Feedbacks);
  
                //.Where( p => p.User.UserId == UserID);
                foreach (var post in _post)
                {
                    // Kiểm tra xem người dùng đã like bài viết hay chưa
                    post.IsLiked = post.Feedbacks.Any(f => f.UserId == UserID && f.Like == true);
                }
                var postFillter = _post.Where(p => p.UserId == UserID).ToList();

                return View(postFillter);
            }
            return RedirectToAction("Login", "Authen");            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteByUserConfirmed(int id)
        {
            if (!PostExists(id))
            {
                return Json(new { success = false });
            }
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post != null)
                {
                    _context.Posts.Remove(post);
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true });

            }
            catch (System.Exception)
            {

                return Json(new { success = false });
            }

        }


        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}