using RecipesWebSite.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebSite.Controllers
{
    [AllowAnonymous]
    public class FoodTypeController : Controller
    {
        // GET: FoodType
        DbRecipeWebSiteEntities1 db = new DbRecipeWebSiteEntities1();
        public ActionResult Index()
        {
            var foodtypes = db.FoodType.ToList();
            return View(foodtypes);
        }
    }
}