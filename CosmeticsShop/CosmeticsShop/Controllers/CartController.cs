using CosmeticsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class CartController : Controller
    {
        ShoppingEntities db = new ShoppingEntities();
        [HttpPost]
        public JsonResult AddItem(int ProductID)
        {
            Product product = db.Products.SingleOrDefault(x => x.ID == ProductID);
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<ItemCart>();
            }
            List<ItemCart> itemCarts = Session["Cart"] as List<ItemCart>;
            // Kiểm tra số lượng tồn
            if (itemCarts.Count > 0 && product.Quantity <= itemCarts.FirstOrDefault(x => x.ProductID == product.ID).Quantity)
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
            // Kiểm tra sản phẩm đã tồn tại trong giỏ hàng chưa
            // Nếu tồn tại thì + số lượng lên 1
            ItemCart check = itemCarts.FirstOrDefault(x => x.ProductID == ProductID);
            if (check != null)
            {
                for (int i = 0; i < itemCarts.Count; i++)
                {
                    if (itemCarts[i].ProductID == ProductID)
                    {
                        itemCarts[i].Quantity += 1;
                    }
                }
            }
            else // Nếu chưa thì thêm mới sản phẩm vào giỏ hàng
            {
                itemCarts.Add(new ItemCart() { ProductID = product.ID, ProductName = product.Name, ProductPrice = product.Price.Value, ProductImage = product.Image1, Quantity = 1 });
            }
            Session["Cart"] = itemCarts;
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTotalCart()
        {
            List<ItemCart> itemCarts = Session["Cart"] as List<ItemCart>;
            return Json(new { TotalPrice = itemCarts.Sum(x => x.ProductPrice * x.Quantity).ToString("#,##"), TotalQuantity = itemCarts.Sum(x => x.Quantity) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateQuantity(int ProductID, int Quantity)
        {
            List<ItemCart> itemCarts = Session["Cart"] as List<ItemCart>;
            // Kiểm tra số lượng tồn
            Product product = db.Products.SingleOrDefault(x => x.ID == ProductID);
            if (itemCarts.Count > 0 && product.Quantity <= Quantity)
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
            for (int i = 0; i < itemCarts.Count; i++)
            {
                if (itemCarts[i].ProductID == ProductID)
                {
                    itemCarts[i].Quantity = Quantity;
                }
            }
            Session["Cart"] = itemCarts;
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSubTotal(int ProductID = 1)
        {
            List<ItemCart> itemCarts = Session["Cart"] as List<ItemCart>;
            return Json(new { SubTotal = itemCarts.Where(x => x.ProductID == ProductID).Sum(x => x.ProductPrice * x.Quantity).ToString("#,##") }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTotal()
        {
            List<ItemCart> itemCarts = Session["Cart"] as List<ItemCart>;
            return Json(new { Total = itemCarts.Sum(x => x.ProductPrice * x.Quantity).ToString("#,##") }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Checkout()
        {
            return View();
        }
    }
}