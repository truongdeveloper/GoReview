using GoReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoReview.Controllers
{
    [ViewComponent(Name = "_Category")]
    public class CategoryViewController : ViewComponent
    {
        private readonly BtlG21Context _context;
        public CategoryViewController (BtlG21Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var _category = _context.Categories.ToList();   
            return View("_Category", _category);
        }
    }
}
