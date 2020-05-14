using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Models;


namespace OrderManagementSystem.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly OrderManagementContext _context;
        private readonly ShoppingCart _shoppingCart;

       
        public CartController(OrderManagementContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            string email = HttpContext.User.Identity.Name;
            Users usr = _context.Userss.Where(a => a.EmailID.Equals(email)).FirstOrDefault();
            List<ShoppingCartItem> items = _shoppingCart.RetrieveUserCart(usr.UserId);
            _shoppingCart.ShoppingCartItems = items;

            var cvm = new CartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(usr.UserId)
            };
            return View(cvm);
        }


        public IActionResult AddToShoppingCart(int id)
        {
            string email = HttpContext.User.Identity.Name;
            Users usr = _context.Userss.Where(a => a.EmailID.Equals(email)).FirstOrDefault();

            _shoppingCart.AddToCart(id, usr.UserId);

            return RedirectToAction("Index");

        }

        public IActionResult RemoveFromShoppingCart(int pid)
        {
            string email = HttpContext.User.Identity.Name;
            Users usr = _context.Userss.Where(a => a.EmailID.Equals(email)).FirstOrDefault();
            _shoppingCart.RemoveFromCart(pid, usr.UserId);
            return RedirectToAction("Index");
        }




    }
}
