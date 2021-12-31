using CosmeticsShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class ProductManageController : Controller
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: ProductManage
        public ActionResult Index(string keyword = "")
        {
            List<Product> products = new List<Product>();
            if (keyword != "")
            {
                products = db.Products.Where(x => x.Name.Contains(keyword)).ToList();
            }
            else
            {
                products = db.Products.Where(x => x.Name.Contains(keyword)).ToList();
            }
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

            for (int i = 0; i < ImageUpload.Length; i++)
            {
                //Check content image
                if (ImageUpload[i] != null && ImageUpload[i].ContentLength > 0)
                {
                    //Get file name
                    var fileName = Path.GetFileName(ImageUpload[i].FileName);
                    //Get path
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    //Check exitst
                    if (!System.IO.File.Exists(path))
                    {
                        //Add image into folder
                        ImageUpload[i].SaveAs(path);
                    }
                }
            }

            if (ImageUpload[0] != null)
            {
                productUpdate.Image1 = ImageUpload[0].FileName;
            }
            if (ImageUpload[1] != null)
            {
                productUpdate.Image2 = ImageUpload[1].FileName;
            }
            if (ImageUpload[2] != null)
            {
                productUpdate.Image3 = ImageUpload[2].FileName;
            }
            db.SaveChanges();

            ViewBag.CategoryList = db.Categories.ToList();
            ViewBag.Message = "Cập nhật thành công";
            return View("Details", productUpdate);
        }
        public ActionResult Edit()
        {
            return RedirectToAction("Index");
        }
    }
}