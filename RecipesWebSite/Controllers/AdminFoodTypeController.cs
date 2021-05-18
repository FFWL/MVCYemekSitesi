using RecipesWebSite.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebSite.Controllers
{
    [AllowAnonymous]
    public class AdminFoodTypeController : Controller
    {
        // GET: AdminFoodType
        DbRecipeWebSiteEntities1 db = new DbRecipeWebSiteEntities1();
        public ActionResult Index()
        {
            var foodtype = db.FoodType.ToList();
            return View(foodtype);
        }

        public void dropdownlist()
        {
            IEnumerable<SelectListItem> foodstatus = (from x in db.FoodTypeStatus.ToList()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.FoodTypeStatus1,
                                                          Value = x.FoodTypeStatusID.ToString()

                                                      }).ToList();
            ViewData["foodstatus"] = foodstatus;
        }

        [HttpGet]
        public ActionResult Update(int id)
        {

            dropdownlist();
            var foodtype = db.FoodType.Find(id);
            return View(foodtype);
        }
        [HttpPost]
        public ActionResult Update(FoodType p)
        {
            int foodtypeid = db.FoodType.Where(x => x.FoodTypeID == p.FoodTypeID).Select(x => x.FoodTypeID).FirstOrDefault();
            FoodType foodtype = db.FoodType.Find(p.FoodTypeID);
            foodtype.FoodType1 = p.FoodType1;
            foodtype.FoodTypeImg = p.FoodTypeImg;
            foodtype.FoodTypeStatusID = Convert.ToBoolean(p.FoodTypeStatus.FoodTypeStatusID);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            FoodType foodtype = db.FoodType.Find(id);
            foodtype.FoodTypeStatusID = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            dropdownlist();
            return View();
        }
        [HttpPost]
        public ActionResult Add(FoodType p)
        {
            FoodType new_foodtype = new FoodType();
            new_foodtype.FoodType1 = p.FoodType1;
            new_foodtype.FoodTypeImg = p.FoodTypeImg;
            new_foodtype.FoodTypeStatusID = Convert.ToBoolean(p.FoodTypeStatus.FoodTypeStatus1);

            db.FoodType.Add(new_foodtype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
