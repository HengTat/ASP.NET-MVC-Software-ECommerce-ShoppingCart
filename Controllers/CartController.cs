using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingCart_9b.DB;
using ShoppingCart_9b.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ShoppingCart_9b.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoppingContext dbcontext;

        public CartController(ShoppingContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public class EditQuantityPost
        {
            public int newQty { get; set; }
            public String cartDetailsId { get; set; }
        }


        [HttpPost]
        public IActionResult EditQty(string json)
        {
            EditQuantityPost data = JsonConvert.DeserializeObject<EditQuantityPost>(json);
            int newQty = data.newQty;
            String cartDetailsId = data.cartDetailsId;
            CartDetails cart = dbcontext.CartDetails.Find(cartDetailsId);
            var oldQty = cart.Qty;
            cart.Qty = newQty;
            dbcontext.Update(cart);
            if (newQty == 0)
            {
                dbcontext.Remove(cart);
            }
            dbcontext.SaveChanges();

            var cartItems = HttpContext.Session.GetString("cartItems");
            if (String.IsNullOrEmpty(cartItems))
            {
                cartItems = "0";
            }
            int parsed = int.Parse(cartItems);
            parsed += newQty - oldQty;
            HttpContext.Session.SetString("cartItems", parsed.ToString());


            string UserId = HttpContext.Session.GetString("UserId");
            var Cart = dbcontext.Cart.Where(x => x.UserId == UserId).FirstOrDefault();
            List<CartDetails> CartDetails = dbcontext.CartDetails.Where(x => x.CartId == Cart.Id).ToList();
            //Get products in cart     
            double productQty;
            double productPx;
            double totalAmt = 0;

            foreach (CartDetails item in CartDetails)
            {
                var productincart = dbcontext.Products.Where(x => x.Id == item.ProductId).FirstOrDefault();
                productQty = item.Qty;
                productPx = productincart.Price;
                totalAmt += (productQty * productPx);
            }

            return Ok(totalAmt);
        }
        public IActionResult Index()
        {

            //Get Cart 
            string UserId = HttpContext.Session.GetString("UserId");
            var Cart = dbcontext.Cart.Where(x => x.UserId == UserId).FirstOrDefault();

            List<CartDetails> CartDetails = new List<CartDetails>();
            if (Cart != null)
            {
                CartDetails = dbcontext.CartDetails.Where(x => x.CartId == Cart.Id).ToList();
            }

            //Get products in cart     
            double productQty;
            double productPx;
            double totalAmt = 0;

            foreach (CartDetails item in CartDetails)
            {
                var productincart = dbcontext.Products.Where(x => x.Id == item.ProductId).FirstOrDefault();
                productQty = item.Qty;
                productPx = productincart.Price;
                totalAmt += (productQty * productPx);
            }

            ViewData["TotalAmt"] = totalAmt;
            return View(CartDetails);
        }

        public IActionResult Checkout()
        {
            //Get Cart 
            string UserId = HttpContext.Session.GetString("UserId");
            var user = dbcontext.User.Find(UserId);
            if (user == null || UserId == "Anonymous")
            {
                TempData["Error"] = "Please Log In Before checking out";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                Order myorder = new Order
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = user.Id,
                    Time = DateTime.Now
                };
                dbcontext.Add(myorder);
                var Cart = dbcontext.Cart.Where(x => x.UserId == UserId).FirstOrDefault();
                List<CartDetails> CartDetails = dbcontext.CartDetails.Where(x => x.CartId == Cart.Id).ToList();
                foreach (CartDetails detail in CartDetails)
                {
                    List<ActivationCode> codes = detail.Product.ActivationCodes.ToList();
                    OrderDetails newOrder = new OrderDetails
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductId = detail.Product.Id,
                        OrderId = myorder.Id,
                        Qty = detail.Qty
                    };
                    dbcontext.Add(newOrder);
                    System.Diagnostics.Debug.WriteLine(detail.Qty.ToString());
                    for (var i = 0; i < detail.Qty; i++)
                    {
                        ActivationCode codeToUse = codes[i];
                        System.Diagnostics.Debug.WriteLine(codeToUse.Id);
                        codeToUse.OrderDetails = newOrder;
                        codeToUse.OrderDetailsId = newOrder.Id;
                        codeToUse.ProductId = null;
                        dbcontext.Update(codeToUse);
                    }
                    dbcontext.Remove(detail);
                    HttpContext.Session.SetString("cartItems", "0");
                }
                dbcontext.SaveChanges();
                return RedirectToAction("Index", "OrderHistory");
            }


        }
        public IActionResult Payment()
        {
            return View();
        }
    }
}
