using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Models;


namespace OrderManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly OrderManagementContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public UsersController(ILogger<UsersController> logger, OrderManagementContext context, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<Users> users = _context.Userss.ToList();
            return View(users);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterUser() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult RegisterUser(Users user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            _context.Userss.Add(user);

            _context.SaveChanges();

            return RedirectToAction("Login", "Accounts");

        }

        public IActionResult UpdateUser(int id)
        {
            Users user = _context.Userss.Where(x => x.UserId == id).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateUser(int id, Users user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteUser(int id)
        {
            Users user = _context.Userss.Where(x => x.UserId == id).FirstOrDefault();
            _context.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}