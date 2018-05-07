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
using Mvc_Repository.Models.Repositiory;

namespace Mvc_Repository.Controllers
{
    public class ProductsController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        private IRepository<Products> productRepository;
        private IRepository<Categories> categoryRepository;

        public IEnumerable<Categories> categories
        {
            get
            {
                return categoryRepository.GetAll();
            }
        }

        public ProductsController()
        {
            this.productRepository = new GenericRepository<Products>();
            this.categoryRepository = new GenericRepository<Categories>();
        }

        // GET: Products
        public ActionResult Index()
        {
            //new
            var products = productRepository.GetAll().ToList();
            return View(products);
        }
        //==========================================================================

        // GET: Products/Details/5
        public ActionResult Details(int id = 0)
        {
            //new 
            Products products = productRepository.Get(x => x.ProductID == id);
            if(products == null)
            {
                return HttpNotFound();
            }
            return View(products);
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
        public ActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                //new 
                this.productRepository.Create(products);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }
        //=================================================================================

        // GET: Products/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Products products = this.productRepository.Get(x => x.ProductID == id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult Edit(Products products)
        {
            
            if (ModelState.IsValid)
            {
                this.productRepository.Update(products);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Products products = this.productRepository.Get(x => x.ProductID == id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = this.productRepository.Get(x => x.ProductID == id);
            this.productRepository.Delete(products);
            return RedirectToAction("Index");
        }
    }
}
