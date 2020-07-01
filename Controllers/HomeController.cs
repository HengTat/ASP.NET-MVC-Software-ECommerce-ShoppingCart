using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingCart_9b.DB;
using ShoppingCart_9b.Models;

namespace ShoppingCart_9b.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShoppingContext dbcontext;

        public HomeController(ShoppingContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginPost(string Username, string Password)
        {
            User user = dbcontext.User
                .Where(m => m.Username == Username).FirstOrDefault();

            if  (user != null && !String.IsNullOrEmpty(Password) && user.IsPWValid(Password))
            {
                HttpContext.Session.SetString("UserId", user.Id);

                //To get username session variable after login

                string UserId = HttpContext.Session.GetString("UserId");
                var loggedinuser = dbcontext.User.Find(UserId);
                var username = loggedinuser.Username;
                HttpContext.Session.SetString("username", username);

                // update user cart and make cart
                var cart = dbcontext.Cart.Where(ord => ord.User == user).FirstOrDefault();
                if (cart == null)
                {
                    cart = new Cart
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = user.Id
                    };
                    dbcontext.Add(cart);
                }
                // update user cart with anonymous cart data

                var anonCart = dbcontext.Cart.Where(ord => ord.UserId == "Anonymous").FirstOrDefault();
                if (anonCart != null)
                {
                    List<CartDetails> cartDeets = dbcontext.CartDetails.Where(cd => cd.CartId == anonCart.Id).ToList();
                    foreach (CartDetails anonDeet in cartDeets) {
                        anonDeet.CartId = cart.Id;
                        dbcontext.Update(anonDeet);
                    }
                    // remove anonymousCart
                    dbcontext.Remove(anonCart);
                }
                dbcontext.SaveChanges();
                //recalculate cart items
                int parsed = 0;
                List<CartDetails> myCartDetails = dbcontext.CartDetails.Where(cd => cd.CartId == cart.Id).ToList();
                foreach (CartDetails detail in myCartDetails)
                {
                    parsed += detail.Qty;
                }
                HttpContext.Session.SetString("cartItems", parsed.ToString());

                TempData["Error"] = "";
                return RedirectToAction("Index", "Products");
            }
            else
            {
                TempData["Error"] = "Invalid Username/Password. Please try again.";
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Login()
        {
            ViewData["Error"] = TempData["Error"];
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
