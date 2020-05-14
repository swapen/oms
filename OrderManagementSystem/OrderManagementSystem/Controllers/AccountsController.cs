using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Models;


namespace OrderManagementSystem.Controllers
{
    public class AccountsController : Controller
    {

        private readonly ILogger<AccountsController> _logger;
        private readonly OrderManagementContext _context;

        [Obsolete]
        public AccountsController(ILogger<AccountsController> logger, OrderManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewData["currentUser"] = HttpContext.User.Identity.Name;
            ViewData["currentClaim"] = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

       /* [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var obj = _context.Userss.Where(a => a.EmailID.Equals(model.EmailID) && a.Pword.Equals(model.Pword)).FirstOrDefault();
                if(obj != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.EmailID)
                    };
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync("CookieAuthenticationDefaults.AuthenticationScheme", principal);
                    return RedirectToAction("Index");
                }
                return View();
            }
            ModelState.AddModelError("", "UserName or Password is blank");
            return View();

        }*/


        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            { 
                if(model.EmailID == "swathi@admin.com" && model.Pword == "password")
                {
                    var id = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    id.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.EmailID));
                    id.AddClaim(new Claim(ClaimTypes.Name, model.EmailID));
                    id.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    var prncpl = new ClaimsPrincipal(id);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, prncpl);
                    return RedirectToAction("Index");
                }


                var obj = _context.Userss.Where(a => a.EmailID.Equals(model.EmailID) && a.Pword.Equals(model.Pword)).FirstOrDefault();
                if (obj == null)
                {
                    ModelState.AddModelError("", "Username or Password is invalid!");
                    return View();
                }
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.EmailID));
                identity.AddClaim(new Claim(ClaimTypes.Name, model.EmailID));
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal/*, new AuthenticationProperties { IsPersistent = model.RememberMe }*/);
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "UserName or Password is blank");
                return View();
            }
        }
        
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var obj = _context.Userss.Where(a => a.EmailID.Equals(user.EmailID) && a.Pword.Equals(user.Pword)).FirstOrDefault();
                if (obj != null)
                {
                    HttpContext.Session.SetInt32("UserId", obj.UserId);
                    HttpContext.Session.SetString("FirstName", obj.FirstName);
                    return RedirectToAction("UserDashBoard");
                }
            }
            else
            {
                ViewBag.error = "Invalid account!!";
            }
            return View(user);
        }*/

        public IActionResult Unauthorize()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
