using RecipesWebSite.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebSite.Controllers
{
    [AllowAnonymous]
    public class AdminFoodController : Controller
    {
        
        // GET: AdminFood
        DbRecipeWebSiteEntities1 db = new DbRecipeWebSiteEntities1();
        public ActionResult Index()
        {
            var foodlist = db.Recipe.ToList();
            return View(foodlist);
        }
        public void dropdownlist()
        {
            IEnumerable<SelectListItem> foodtypes = (from x in db.FoodType.ToList()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.FoodType1,
                                                          Value = x.FoodTypeID.ToString()

                                                      }).ToList();
            ViewData["foodtypes"] = foodtypes;
        }
        [HttpGet]
        public ActionResult Update(int id)
        {

            dropdownlist();
            var food = db.Recipe.Find(id);
            return View(food);
        }
        [HttpPost]
        public ActionResult Update(Recipe p,string Foodingridient)
        {
            Recipe recipe = db.Recipe.Find(p.RecipeID);
            var ingID = db.Ingridient.Where(x => x.RecipeID == p.RecipeID).Select(x=>x.IngridientsID).FirstOrDefault();
            Ingridient ingridient = db.Ingridient.Find(ingID);
            recipe.RecipeName = p.RecipeName;
            recipe.RecipeDescription = p.RecipeDescription;
            recipe.RecipeImg = p.RecipeImg;
            ingridient.Foodingridient = Foodingridient;
            recipe.FoodTypeID= Convert.ToInt32(p.FoodType.FoodType1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            Recipe recipe = db.Recipe.Find(id);
            int ingridientid = db.Ingridient.Where(x => x.RecipeID == recipe.RecipeID).Select(x => x.IngridientsID).FirstOrDefault();
            Ingridient ıngridient = db.Ingridient.Find(ingridientid);
            db.Recipe.Remove(recipe);
            db.Ingridient.Remove(ıngridient);
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
        public ActionResult Add(Recipe p, string Foodingridient)
        {
            Recipe new_recipe = new Recipe();
            new_recipe.RecipeName = p.RecipeName;
            new_recipe.RecipeDescription = p.RecipeDescription;
            new_recipe.RecipeImg = p.RecipeImg;
            new_recipe.FoodTypeID = Convert.ToInt32(p.FoodType.FoodType1);
            db.Recipe.Add(new_recipe);
            db.SaveChanges();
            Ingridient ingridient = new Ingridient();
            ingridient.RecipeID = new_recipe.RecipeID;
            ingridient.Foodingridient = Foodingridient;
            db.Ingridient.Add(ingridient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}