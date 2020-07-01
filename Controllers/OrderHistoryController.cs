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
    public class OrderHistoryController : Controller
    {
        private readonly ShoppingContext dbcontext;
        public OrderHistoryController(ShoppingContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public static List<Order> OrderHistory;
        public IActionResult Index()
        {
            //Get UserId

            string UserId = HttpContext.Session.GetString("UserId");
            //Get List of Orders in History
            List<Order> OrderHistory = dbcontext.Order.Where(x => x.UserId == UserId).ToList();
            //Get List of Total List of products
            List<OrderDetails> TotalList = new List<OrderDetails>();
            foreach (Order order in OrderHistory)
            {
                List<OrderDetails> Listoforderdetails = dbcontext.OrderDetails.Where(x => x.OrderId == order.Id).ToList();
                foreach (var OrderDetails in Listoforderdetails)
                {
                    TotalList.Add(OrderDetails);
                }

            }
            ViewData["Activationcode"] = 425425425;
            ViewData["UserId"] = HttpContext.Session.GetString("UserId");
            return View(TotalList);
        }
    }
}