using GoReview.Data;
using Microsoft.AspNetCore.Mvc;

namespace GoReview.Controllers
{
    [ViewComponent(Name ="_Category")]
    public class CategoryViewComponent : ViewComponent
    {

        private readonly GoReviewContext _context;

        public CategoryViewComponent(GoReviewContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()    
        {
            var _category = _context.Category.ToList();
            return View("_Category",_category);
        }
    }
}
