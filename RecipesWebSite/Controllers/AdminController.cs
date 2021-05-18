using RecipesWebSite.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebSite.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        // GET: Admin
        DbRecipeWebSiteEntities1 db = new DbRecipeWebSiteEntities1();
        public ActionResult Index()
        {
            var user = db.User.ToList();
            return View(user);
        }
        public void dropdownlists()
        {
            IEnumerable<SelectListItem> status = (from x in db.UserStatus.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.UserStatus1,
                                                      Value = x.UserStatusID.ToString()

                                                  }).ToList();
            ViewData["status"] = status;

            IEnumerable<SelectListItem> level = (from x in db.UserLevel.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.UserLevel1,
                                                     Value = x.UserLevelID.ToString()

                                                 }).ToList();
            ViewData["level"] = level;
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            dropdownlists();
            var user = db.User.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Update(User p)
        {
            User user = db.User.Find(p.UserID);
            user.UserName = p.UserName;
            user.UserSurname = p.UserSurname;
            user.UserNickname = p.UserNickname;
            user.UserPassword = p.UserPassword;
            user.UserStatusID = Convert.ToBoolean(p.UserStatus.UserStatus1);
            user.UserLevelID =Convert.ToByte(p.UserLevel.UserLevel1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            User user = db.User.Find(id);
            user.UserStatusID = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Add()
        {
            dropdownlists();
            return View();
        }
        [HttpPost]
        public ActionResult Add(User p)
        {
            User new_user = new User();
            new_user.UserName = p.UserName;
            new_user.UserSurname = p.UserSurname;
            new_user.UserNickname = p.UserNickname;
            new_user.UserPassword = p.UserPassword;
            new_user.UserLevelID = Convert.ToByte(p.UserLevel.UserLevel1);
            new_user.UserStatusID = Convert.ToBoolean(p.UserStatus.UserStatus1);

            db.User.Add(new_user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}