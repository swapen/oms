using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManagementSystem.Models
{
    public class ShoppingCart
    {
        private OrderManagementContext _context;

        public ShoppingCart(OrderManagementContext context)
        {
            _context = context;
        }

        public ShoppingCart()
        {
        }

        //public string ShoppingCartID { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }



        /*public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var contexts = services.GetService<OrderManagementContext>();
            string cartId = session.GetString("CartId");
            if(cartId == null)
            {
                cartId = Guid.NewGuid().ToString();
            }
            session.SetString("CartId", cartId);
            return new ShoppingCart(contexts) { ShoppingCartId = cartId };
        }*/

   
        public void AddToCart(int productid, int userid)
        {
            Products product = _context.Productss.Where(x => x.ProductID == productid).FirstOrDefault();
            
            var shoppingCartItem = _context.ShoppingCartItemss
                                           .Where(x => x.ProductId == productid
                                           && x.UserId == userid).FirstOrDefault();

            if (shoppingCartItem == null)
            {
            shoppingCartItem = new ShoppingCartItem
            {
                ProductId = productid,
                UserId = userid,
                NumberOfUnits = 1,
                Price = product.Price,
            };

                _context.ShoppingCartItemss.Add(shoppingCartItem);

            }
            else
            {
                shoppingCartItem.NumberOfUnits++;
            }
            _context.SaveChanges();
        }
        
        public void RemoveFromCart(int pid, int uid)
        {
            var shoppingCartItem = _context.ShoppingCartItemss.Where(s => s.ProductId == pid && s.UserId == uid).FirstOrDefault();

            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.NumberOfUnits > 1)
                {
                    shoppingCartItem.NumberOfUnits--;
                    _context.ShoppingCartItemss.Update(shoppingCartItem);
                }
                else
                {
                    _context.ShoppingCartItemss.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();

            return;
        }

        public void ClearCart(int uid)
        {
            var cartItems = _context.ShoppingCartItemss.Where(cart => cart.UserId == uid);

            _context.ShoppingCartItemss.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public List<ShoppingCartItem> RetrieveUserCart(int id)
        {
            List<ShoppingCartItem> currentCart = _context.ShoppingCartItemss.Where(c => c.UserId == id).ToList();
            return currentCart;
        }

        public decimal GetShoppingCartTotal(int uid)
        {
            var total = _context.ShoppingCartItemss.Where(c => c.UserId == uid)
                            .Select(c => c.product.Price * c.NumberOfUnits).Sum();
            return total;
        }
    }

}
