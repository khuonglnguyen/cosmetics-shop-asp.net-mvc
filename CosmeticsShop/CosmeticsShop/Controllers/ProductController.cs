using CosmeticsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class ProductController : Controller
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.ListCategory = db.Categories.ToList();
            ViewBag.ListProduct = db.Products.ToList();
            return View();
        }
        public ActionResult Details(int ID)
        {
            ViewBag.ListCategory = db.Categories.ToList();
            Product product = db.Products.Single(x => x.ID == ID);
            return View(product);
        }
    }
}