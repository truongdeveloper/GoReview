using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoReview.Data;
using GoReview.Models;
using System.Xml.Linq;
using Microsoft.Extensions.Hosting;

namespace GoReview.Controllers
{
    public class PostsController : Controller
    {
        private readonly GoReviewContext _context;

        public PostsController(GoReviewContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var goReviewContext = _context.Post.Include(p => p.Category).Include(p => p.User);
            return View(await goReviewContext.ToListAsync());
        }
        //Get/SearchPost
        public async Task<IActionResult> SearchPost(string name)
        {
            if (name == null || _context.Post == null)
            {
                return NotFound();
            }

             List<Post> post = _context.Post
                .Include(p => p.Category)
                .Include(p => p.User)
                .Where(m=>m.Content.Contains(name)).ToList();
            return View(post);
        }
        //tim bang PostbyCategory
        public async Task<IActionResult> PostbyCategory(int? CatID)
        {
            var goReviewContext = _context.Post.Include(p => p.Category).Include(p => p.User).Where(m=>m.CategoryId==CatID);
            return View(await goReviewContext.ToListAsync());
        }
        //Get/PostDetails/id
        public async Task<IActionResult> PostDetails(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId","Title");
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "FullName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,UserID,CategoryId,Title,Content,Picture,CreateDate,NumLike,NumComment")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Image", post.CategoryId);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "FullName", post.UserID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Title", post.CategoryId);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "FullName", post.UserID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,UserID,CategoryId,Title,Content,Picture,CreateDate,NumLike,NumComment")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Title", post.CategoryId);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "FullName", post.UserID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Post == null)
            {
                return Problem("Entity set 'GoReviewContext.Post'  is null.");
            }
            var post = await _context.Post.FindAsync(id);
            if (post != null)
            {
                _context.Post.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Select Comment 
        public JsonResult DsComment(int postID)
        {
            List<Comment> comments = _context.Comment.Include(c => c.Post).Include(c => c.User).Where(m=>m.PostId == postID)
                .OrderBy(n=>n.CommentText)
                .Select(n=> 
                new Comment
                {
                    CommentId = n.CommentId,
                    CommentText = n.CommentText
                }
                ).ToList();
            return Json(comments);
        }

        private bool PostExists(int id)
        {
          return (_context.Post?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
      
    }
}
