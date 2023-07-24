using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoReview.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GoReview.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly BtlG21Context _context;

        public FeedbacksController(BtlG21Context context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            var btlG21Context = _context.Feedbacks.Include(f => f.Post).Include(f => f.User);
            return View(await btlG21Context.ToListAsync());
        }

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Feedbacks == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Post)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserEmail");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeedbackId,PostId,UserId,Like,Comment")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId", feedback.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserEmail", feedback.UserId);
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Feedbacks == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId", feedback.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserEmail", feedback.UserId);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedbackId,PostId,UserId,Like,Comment")] Feedback feedback)
        {
            if (id != feedback.FeedbackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.FeedbackId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId", feedback.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserEmail", feedback.UserId);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Feedbacks == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Post)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Feedbacks == null)
            {
                return Problem("Entity set 'BtlG21Context.Feedbacks'  is null.");
            }
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id)
        {
          return (_context.Feedbacks?.Any(e => e.FeedbackId == id)).GetValueOrDefault();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(int postId, string comment)
        {
            int loggedInUserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Feedback newFeedback = new Feedback
            {
                PostId = postId,
                Like = false,
                UserId = loggedInUserId,
                Comment = comment
            };

            _context.Feedbacks.Add(newFeedback);
            _context.SaveChanges();

            return Json(new
            {
                ImageUser = User.FindFirstValue("ImageUser"),
                UserName = User.FindFirstValue("Name"),
                Comment = comment
            }); // Trả về thông báo bình luận dưới dạng JSON
        }

        //[HttpPost]
        //public IActionResult GetMoreComments(int postId, int page)
        //{
        //    Lấy dữ liệu bình luận từ cơ sở dữ liệu hoặc từ bất kỳ nguồn dữ liệu nào khác
        //   var pageSize = 10; // Số bình luận hiển thị trong mỗi trang
        //    var comments = _context.Comments.Where(c => c.PostId == postId)
        //                                    .Skip((page - 1) * pageSize)
        //                                    .Take(pageSize)
        //                                    .ToList();

        //    Trả về dữ liệu dưới dạng JSON
        //    return Json(comments);
        //}

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LikeActionAsync(int id)
        {
            try
            {
                // Kiểm tra xem người dùng đã đăng nhập chưa
                if (!User.Identity.IsAuthenticated)
                {
                    return Json(new { success = false, message = "Bạn cần đăng nhập để thực hiện hành động này." });
                }

                IQueryable<Post> query = _context.Posts.Include(p => p.Cat).Include(p => p.User).Include(p => p.Feedbacks);
                // Lấy ID của người dùng đăng nhập
                int loggedInUserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                // Tìm bài viết dựa trên postId
                Post post = query.FirstOrDefault(p => p.PostId == id);

                // Kiểm tra xem bài viết có tồn tại không
                if (post == null)
                {
                    return Json(new { success = false, message = "Bài viết không tồn tại." });
                }

                
                // Kiểm tra xem người dùng đã like bài viết hay chưa
                bool isLiked = post.Feedbacks.Any(f => f.UserId == loggedInUserId && f.Like == true);

                if (!isLiked)
                {
                    // Nếu chưa like thì thêm like vào bài viết
                    Feedback feedback = new Feedback
                    {
                        UserId = loggedInUserId,
                        PostId = id,
                        Like = true,
                        Comment = null,
                    };

                    _context.Feedbacks.Add(feedback);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Nếu đã like rồi thì hủy like
                    var feedback = post.Feedbacks.FirstOrDefault(f => f.UserId == loggedInUserId && f.Like == true);
                    _context.Feedbacks.Remove(feedback);
                    await _context.SaveChangesAsync();
                }

                // Trả về kết quả thành công
                return Json(new { isLiked = !isLiked, });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return Json(new { success = false, message = "Đã xảy ra lỗi khi thực hiện hành động like." });
            }
        }
    }
}
