using CosmeticsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class OrderManageController : Controller
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: OrderManage
        public ActionResult Index()
        {
            List<Order> orders = db.Orders.ToList();
            return View(orders);
        }
        public ActionResult Details(int ID)
        {
            ViewBag.IsProcessed = (db.Orders.Find(ID).Status == "Processing") ? false : true;
            List<OrderDetail> orderDetails = db.OrderDetails.Where(x => x.OrderID.Value == ID).ToList();
            return View(orderDetails);
        }
        public ActionResult Processed(int ID)
        {
            Order order = db.Orders.Find(ID);
            order.Status = "Processed";
            order.DateShip = DateTime.Now.AddDays(3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delivering(int ID)
        {
            Order order = db.Orders.Find(ID);
            order.Status = "Delivering";
            order.DateShip = DateTime.Now.AddDays(3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}