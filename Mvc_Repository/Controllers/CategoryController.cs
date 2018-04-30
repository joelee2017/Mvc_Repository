using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Repository.Models;

namespace Mvc_Repository.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                var query = db.Categories.OrderBy(x => x.CategoryID);
                ViewData.Model = query.ToList();
                return View();
            }
        }

        //=================================================================

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                using (NorthwindEntities db = new NorthwindEntities())
                {
                    var model = db.Categories.FirstOrDefault(x => x.CategoryID == id.Value);
                    ViewData.Model = model;
                    return View();
                }
            }
        }

        //=====================================================================
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categories category)
        {
            if(category != null && ModelState.IsValid)
            {
                using (NorthwindEntities db = new NorthwindEntities())
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                }
                return RedirectToAction("index");
            }
            else
            {
                return View(category);
            }
        }

        //=====================================================================
        public ActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                using (NorthwindEntities db = new NorthwindEntities())
                {
                    var model = db.Categories.FirstOrDefault(x => x.CategoryID == id.Value);
                    ViewData.Model = model;
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(Categories category)
        {
            if(category != null && ModelState.IsValid)
            {
                using (NorthwindEntities db = new NorthwindEntities())
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return View(category);
                }
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        //=====================================================================================

        public ActionResult Delete(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                using (NorthwindEntities db = new NorthwindEntities())
                {
                    var model = db.Categories.FirstOrDefault(x => x.CategoryID == id.Value);
                    ViewData.Model = model;
                    return View();
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                using (NorthwindEntities db = new NorthwindEntities())
                {
                    var target = db.Categories.FirstOrDefault(x => x.CategoryID == id);
                    db.Categories.Remove(target);
                    db.SaveChanges();
                }
            }
            catch(DataException)
            {
                return RedirectToAction("Delect", new { id = id });
            }
            return RedirectToAction("index");
        }
    }
}