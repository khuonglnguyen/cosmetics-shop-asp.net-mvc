using CosmeticsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class ProductManageController : Controller
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: ProductManage
        public ActionResult Index()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
        }
        public ActionResult ToggleActive(int ID)
        {
            Product product = db.Products.Find(ID);
            product.IsActive = !product.IsActive;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int ID)
        {
            Product product = db.Products.Find(ID);
            ViewBag.CategoryList = db.Categories.ToList();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase[] ImageUpload)
        {
            Product productUpdate = db.Products.Find(product.ID);
            productUpdate.Name = product.Name;
            productUpdate.Price = product.Price;
            productUpdate.Quantity = product.Quantity;
            productUpdate.CategoryID = product.CategoryID;
            productUpdate.Description = product.Description;
            if (ImageUpload[0] != null)
            {
                productUpdate.Image1 = ImageUpload[0].FileName;
            }
            if (ImageUpload[1] != null)
            {
                productUpdate.Image1 = ImageUpload[1].FileName;
            }
            if (ImageUpload[2] != null)
            {
                productUpdate.Image1 = ImageUpload[2].FileName;
            }
            db.SaveChanges();

            ViewBag.CategoryList = db.Categories.ToList();
            return View("Details", productUpdate);
        }
    }
}