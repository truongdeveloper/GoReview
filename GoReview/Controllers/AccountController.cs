using GoReview.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using GoReview.Data;

namespace GoReview.Controllers
{
    public class AccountController : Controller
    {
        private readonly GoReviewContext _context;
        // tao contructor Account
        public AccountController(GoReviewContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        //xu ly login
        [HttpPost]
        public IActionResult Login(User _userFormPage)
        {
            //kiem tra dieu kien user trong bang co match vs user nhan duoctu form
            var _user = _context.User.Where(m => m.UserEmail == _userFormPage.UserEmail && m.UserPassword == _userFormPage.UserPassword).FirstOrDefault();
            if (_user == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, _user.UserEmail),
                        new Claim("FullName", _user.UserName),
                        new Claim(ClaimTypes.Role, _user.UserRole),
                    };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index", "Account");

            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }
    }
}
