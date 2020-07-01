using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart_9b.DB;
using ShoppingCart_9b.Models;

namespace ShoppingCart_9b.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShoppingContext dbcontext;

        public ProductsController(ShoppingContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        // GET: Products
        public IActionResult Index(string searchString)
        {
            var products = dbcontext.Products.AsEnumerable();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                products = products.Where(prod => prod.Name.ToLower().Contains(searchString) || prod.Description.ToLower().Contains(searchString));
            }
            ViewData["searched"] = searchString;
            return View(products.ToList());
        }



        // GET: Products/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = dbcontext.Products
                .Where(m => m.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }


            List<ProductReview> productReviews = dbcontext.ProductReview
                .Where(m => m.ProductId == id).ToList();
            ViewData["Reviews"] = productReviews;

            List<Product> Recommendations = dbcontext.Products
                .Take(3).ToList();
            ViewData["Recommendations"] = Recommendations;

            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(string id)
        {
            string parsedUserId = HttpContext.Session.GetString("UserId");
            var user = dbcontext.User.Find(parsedUserId);
            if (user == null)
            {
                user = new User
                {
                    Id = "Anonymous",
                    Username = "Anonymous",
                    Password = "Anonymous"
                };
                dbcontext.Add(user);
                HttpContext.Session.SetString("UserId", user.Id);
            }
            if (user != null)
            {
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
                var existingCartDeet = dbcontext.CartDetails.Where(cd => cd.CartId == cart.Id && cd.ProductId == id).FirstOrDefault();
                if (existingCartDeet == null)
                {
                    existingCartDeet = new CartDetails
                    {
                        Id = Guid.NewGuid().ToString(),
                        CartId = cart.Id,
                        ProductId = id,
                        Qty = 1
                    };
                    dbcontext.Add(existingCartDeet);
                }
                else
                {
                    existingCartDeet.Qty += 1;
                    dbcontext.Update(existingCartDeet);
                }
                dbcontext.SaveChanges();

                var cartItems = HttpContext.Session.GetString("cartItems");
                if (String.IsNullOrEmpty(cartItems))
                {
                    cartItems = "0";
                }
                int parsed = int.Parse(cartItems);
                parsed += 1;
                HttpContext.Session.SetString("cartItems",parsed.ToString());
                return Ok(cart);
            }
            else
            {
                return NotFound();
            }
        }
    }
}