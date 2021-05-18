using RecipesWebSite.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebSite.Controllers
{
    [AllowAnonymous]
    public class RecipeController : Controller
    {
        // GET: Recipe
        DbRecipeWebSiteEntities1 db = new DbRecipeWebSiteEntities1();
        public ActionResult MainMenu()
        {
            return View();
        }
        public ActionResult Index(int id)
        {
            var recipes = db.Recipe.Where(x=>x.FoodTypeID == id).ToList();
            return View(recipes);
        }

        public ActionResult Details(int id)
        {
            var recipe = db.Recipe.Where(x => x.RecipeID == id).FirstOrDefault();
            ViewBag.description = recipe.RecipeDescription;
            ViewBag.ingridients = db.Ingridient.Where(x => x.RecipeID == id).Select(x => x.Foodingridient).FirstOrDefault();
            return View(recipe);
        }
    }
}