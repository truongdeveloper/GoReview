using GoReview.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace GoReview.Controllers
{
    public class AuthenController : Controller
    {
        private readonly BtlG21Context _context;

        public AuthenController(BtlG21Context context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User _userFormPage)
        {
            //kiem tra dieu kien user trong bang co match vs user nhan duoctu form
            var _user = _context.Users.Where(m => m.UserEmail == _userFormPage.UserEmail && m.UserPassword == _userFormPage.UserPassword).FirstOrDefault();
            if (_user == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.NameIdentifier, _user.UserId.ToString()),
                        new Claim("UserId", _user.UserId.ToString()),
                        new Claim("Name", _user.UserName),
                        new Claim("FullName", _user.FullName),
                        new Claim("ImageUser", _user.ImageUser),
                        new Claim("UserEmail", _user.UserEmail),
                        new Claim("Introduce", _user.Introduce),
                        new Claim("UserRole", _user.UserRole),
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
                return RedirectToAction("Index", "Home");

            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

    }
}