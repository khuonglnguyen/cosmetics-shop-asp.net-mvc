using CosmeticsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class HomeController : Controller
    {
        ShoppingEntities db = new ShoppingEntities();
        public ActionResult Index()
        {
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<ItemCart>();
            }
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Models.User user)
        {
            try
            {
                user.Captcha = new Random().Next(100000, 999999).ToString();
                user.IsConfirm = false;
                db.Users.Add(user);
            }
            catch (Exception)
            {
                ViewBag.Message = "Đăng ký thất bại";
            }
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string Email, string Password)
        {
            Models.User check = db.Users.SingleOrDefault(x => x.Email == Email && x.Password == Password);
            if (check != null)
            {
                Session["User"] = check;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Email hoặc mật khẩu không đúng";
            return View();
        }
        public ActionResult SignOut()
        {
            Session.Remove("User");
            return RedirectToAction("Index");
        }
    }
}