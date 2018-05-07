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
using Mvc_Repository.Web.Models;

namespace Mvc_Repository.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;

        public IEnumerable<Categories> Categories
        {
            get
            {
                return categoryRepository.GetAll();
            }
        }

        public ProductsController()
        {
            this.productRepository = new ProductRepository();
            this.categoryRepository = new CategoryRepository();
        }

        // GET: Products
        public ActionResult Index(string category = "all")
        {
            int categoryID = 1;

            ViewBag.CategorySelectList = int.TryParse(category, out categoryID)
                ? this.CategorySelectList(categoryID.ToString())
                : this.CategorySelectList("all");

            var products = productRepository.GetAll().ToList();
            return View(products);
        }

        public List<SelectListItem> CategorySelectList(string selectedValue = "all")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() {
                Text = "All Category",
                Value = "all",
                Selected = selectedValue.Equals("all", StringComparison.OrdinalIgnoreCase)
            });

            var categories = categoryRepository.GetAll().OrderBy(x => x.CategoryID);

            foreach(var c in categories)
            {
                items.Add(new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryID.ToString(),
                    Selected = selectedValue.Equals(c.CategoryID.ToString())
                });
            }
            return items;
        }
        //==========================================================================

        // GET: Products/Details/5
        public ActionResult Details(int id = 0)
        {
            //new 
            Products products = productRepository.GetByID(id);
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
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName");
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
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }
        //=================================================================================

        // GET: Products/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Products products = this.productRepository.GetByID(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
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
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Products products = this.productRepository.GetByID(id);
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
            Products products = this.productRepository.GetByID(id);
            this.productRepository.Delete(products);
            return RedirectToAction("Index");
        }
    }
}
