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
    }
}