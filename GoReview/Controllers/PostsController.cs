using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoReview.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GoReview.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PostsController : Controller
    {
        private readonly BtlG21Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostsController(BtlG21Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var goReviewContext = _context.Posts.Include(p => p.Cat).Include(p => p.User);
            return View(await goReviewContext.ToListAsync());
        }


        // GET: Posts/Create
        public IActionResult Create()
        {
            if(User.Identity.IsAuthenticated)
            {
                ViewData["CatId"] = new SelectList(_context.Categories, "CategoryId", "Title");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Authen");
            }
            
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,Title,Content")] Post post, IFormFile Picture)
        {
            try
            {
                if (Picture != null && Picture.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Picture.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Picture.CopyTo(fileStream);
                    }

                    post.Picture = fileName;
                }
                post.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                post.CreateDate = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Profile");

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CatId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,UserId,CatId,Title,Content,Picture,CreateDate")] Post post)
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
            ViewData["CatId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CatId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", post.UserId);
            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var user = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'BtlG21Context.Users'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
