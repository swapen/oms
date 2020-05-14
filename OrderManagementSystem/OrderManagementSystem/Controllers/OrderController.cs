using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderManagementSystem.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        private readonly OrderManagementContext _context;
        private readonly ShoppingCart _shoppingCart;


        public OrderController(OrderManagementContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        [HttpGet]
        public IActionResult CheckOut()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel vm)
        {
            if(ModelState.IsValid)
            {
                string session = HttpContext.User.Identity.Name;
                Users usr = _context.Userss.Where(a => a.EmailID.Equals(session)).FirstOrDefault();

                //Entering values into the OrderDetails database
                string uniqueID = Guid.NewGuid().ToString();
                List<ShoppingCartItem> currentcart = _shoppingCart.RetrieveUserCart(usr.UserId);
                foreach (var item in currentcart)
                {
                    OrderDetails details = new OrderDetails
                    {
                        SessionID = session,
                        OrderDetailID = uniqueID,
                        ProductId = item.ProductId,
                        UserName = (usr.FirstName + usr.LastName),
                        NumberOfUnits = item.NumberOfUnits,
                        Price = item.Price
                    };
                    _context.OrderDetailss.Add(details);
                    _context.SaveChanges();
                }

                //Entering values into the Order table using view model
                Order order = new Order();

                order.SessionID = session;
                order.OrderDetailID = uniqueID;
                order.UserID = usr.UserId;
                order.FirstName = vm.FirstName;
                order.LastName = vm.LastName;
                order.AddressLine1 = vm.AddressLine1;
                order.AddressLine2 = vm.AddressLine2;
                order.ZipCode = vm.ZipCode;
                order.State = vm.State;
                order.Country = vm.Country;
                order.PhoneNumber = vm.PhoneNumber;
                order.Email = vm.Email;
                order.OrderTotal = _shoppingCart.GetShoppingCartTotal(usr.UserId);
                order.TimeOfOrder = DateTime.Now;

                
                _context.Orderss.Add(order);
                _context.SaveChanges();
                _shoppingCart.ClearCart(usr.UserId);
                return RedirectToAction("CheckOutComplete");
            }
            return View();
        }

        public IActionResult CheckOutComplete()
        {
            return View();
        }

        /*public IActionResult RetrieveAllOrders()
        {
            //gives the user details of the order, order total and time of the order.
            List<Order> listOrder = _context.Orderss.Where(o => o.SessionID == HttpContext.User.Identity.Name).ToList();
            RetrieveOrderViewModel rvm = new RetrieveOrderViewModel();
            foreach (var item in listOrder)
            {
                ovm.TimeOfOrder = item.TimeOfOrder;
                ovm.
            }
            //List<OrderDetails> listDetails = _context.OrderDetailss.Where(d => d.)


            return View();
        }

        public IActionResult ViewCurrentOrder()
        {
            string currentSession = HttpContext.User.Identity.Name;
            //Order currentOrder = _context.Orderss.Where(o => o.TimeOfOrder ==(_context.Orderss.)) 
            return View();
        }*/


        public IActionResult UpdateInventory()
        {
            return View();
        }


    }
}
