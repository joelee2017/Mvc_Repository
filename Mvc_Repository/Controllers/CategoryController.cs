using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Repository.Models;
using Mvc_Repository.Models.Interface;

namespace Mvc_Repository.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;

        public CategoryController()
        {
            this.categoryRepository = new Models.Repositiory.CategoryRepository();
        }


        // GET: Category
        public ActionResult Index()
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                //new 
                var categories = this.categoryRepository.GetAll().ToList();
                return View(categories);

                //old
                //var query = db.Categories.OrderBy(x => x.CategoryID);
                //ViewData.Model = query.ToList();
                //return View();
            }
        }

        //=================================================================

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //new
                var category = this.categoryRepository.Get(id.Value);
                return View(category);

                //old
                //using (NorthwindEntities db = new NorthwindEntities())
                //{
                //    var model = db.categories.firstordefault(x => x.categoryid == id.value);
                //    viewdata.model = model;
                //    return view();
                //}
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
                //new 
                this.categoryRepository.Create(category);
                return RedirectToAction("Index");

                //old
                //using (NorthwindEntities db = new NorthwindEntities())
                //{
                //    db.Categories.Add(category);
                //    db.SaveChanges();
                //}
                //return RedirectToAction("index");
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
                return RedirectToAction("Index");
            }
            else
            {
                //new
                var category = this.categoryRepository.Get(id.Value);
                return View(category);

                //old
                //using (NorthwindEntities db = new NorthwindEntities())
                //{
                //    var model = db.Categories.FirstOrDefault(x => x.CategoryID == id.Value);
                //    ViewData.Model = model;
                //    return View();
                //}
            }
        }

        [HttpPost]
        public ActionResult Edit(Categories category)
        {
            if(category != null && ModelState.IsValid)
            {
                //new 
                this.categoryRepository.Update(category);
                return View(category);

                //old
                //using (NorthwindEntities db = new NorthwindEntities())
                //{
                //    db.Entry(category).State = EntityState.Modified;
                //    db.SaveChanges();
                //    return View(category);
                //}
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //=====================================================================================

        public ActionResult Delete(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //new 
                var category = this.categoryRepository.Get(id.Value);
                return View(category);

                //old
                //using (NorthwindEntities db = new NorthwindEntities())
                //{
                //    var model = db.Categories.FirstOrDefault(x => x.CategoryID == id.Value);
                //    ViewData.Model = model;
                //    return View();
                //}
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //new
                var category = this.categoryRepository.Get(id);
                this.categoryRepository.Delete(category);

                //old
                //using (NorthwindEntities db = new NorthwindEntities())
                //{
                //    var target = db.Categories.FirstOrDefault(x => x.CategoryID == id);
                //    db.Categories.Remove(target);
                //    db.SaveChanges();
                //}
            }
            catch(DataException)
            {
                return RedirectToAction("Delect", new { id = id });
            }
            return RedirectToAction("Index");
        }
    }
}