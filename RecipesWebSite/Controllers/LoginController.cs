using RecipesWebSite.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RecipesWebSite.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        DbRecipeWebSiteEntities1 db = new DbRecipeWebSiteEntities1();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User p)
        {
            var userinfo = db.User.FirstOrDefault(x => x.UserNickname == p.UserNickname && x.UserPassword == p.UserPassword);
            if(userinfo != null)
            {
                FormsAuthentication.SetAuthCookie(userinfo.UserNickname, false);
                Session["UserNickname"] = userinfo.UserNickname.ToString();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}