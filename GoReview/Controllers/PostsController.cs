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

namespace GoReview.Controllers
{
    public class PostsController : Controller
    {
        private readonly BtlG21Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostsController(BtlG21Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CategoryId", "Title");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,Title,Content")] Post post, IFormFile Picture)
        {

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Profile");
            }
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
            post.UserId = 1;
            post.CreateDate = DateTime.Now;
            _context.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Profile");

 
            //ViewData["CatId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CatId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", post.UserId);
            //return View(post);
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


        // POST: Posts/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
