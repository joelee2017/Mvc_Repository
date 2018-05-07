using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc_Repository.Models;
using Mvc_Repository.Models.Interface;

namespace Mvc_Repository.Controllers
{
    public class ProductsController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;

        public IEnumerable<Categories> categories
        {
            get
            {
                return categoryRepository.GetAll();
            }
        }

        public ProductsController()
        {
            this.productRepository = new Models.Repositiory.ProductRepository();
            this.categoryRepository = new Models.Repositiory.CategoryRepository();
        }

        // GET: Products
        public ActionResult Index()
        {
            //new
            var products = productRepository.GetAll().ToList();
            return View(products);

            //old
            //var products = db.Products.Include(p => p.Categories);
            //return View(products.ToList());
        }
        //==========================================================================

        // GET: Products/Details/5
        public ActionResult Details(int id = 0)
        {
            //new 
            Products products = productRepository.Get(id);
            if(products == null)
            {
                return HttpNotFound();
            }
            return View(products);
            //old
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Products products = db.Products.Find(id);
            //if (products == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(products);
        }
        //===============================================================================

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                //new 
                this.productRepository.Create(products);
                return RedirectToAction("Index");

                //old
                //db.Products.Add(products);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }
        //=================================================================================

        // GET: Products/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Products products = this.productRepository.Get(id);            
            if (products == null)
            {
                //new
                return HttpNotFound();
                //old
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //old
            //Products products = db.Products.Find(id);
            //if (products == null)
            //{
            //    return HttpNotFound();
            //}
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Products products)
        {
            
            if (ModelState.IsValid)
            {
                //new 
                this.productRepository.Update(products);
                return RedirectToAction("Index");
                //old
                //db.Entry(products).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Products products = this.productRepository.Get(id);
            if (products == null)
            {
                //new
                return HttpNotFound();

                //old
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Products products = db.Products.Find(id);
            //if (products == null)
            //{
            //    return HttpNotFound();
            //}
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //new
            Products products = this.productRepository.Get(id);
            this.productRepository.Delete(products);
            //old
            //Products products = db.Products.Find(id);
            //db.Products.Remove(products);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
