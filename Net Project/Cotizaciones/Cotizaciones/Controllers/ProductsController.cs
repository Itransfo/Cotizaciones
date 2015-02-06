using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cotizaciones.Models;

namespace Cotizaciones.Controllers
{
    [Authorize(Roles = "authorized-user")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = String.IsNullOrEmpty(sortOrder) ? "nameAsc" : sortOrder;
            var products = from p in db.Products select p;
            switch (sortOrder)
            {
                case "nameDesc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "providerDesc":
                    products = products.OrderByDescending(p => p.Provider);
                    break;
                case "providerAsc":
                    products = products.OrderBy(p => p.Provider);
                    break;
                case "familyDesc":
                    products = products.OrderByDescending(p => p.ProductFamily);
                    break;
                case "familyAsc":
                    products = products.OrderBy(p => p.ProductFamily);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "admin, management, sales-admin, operations-admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, management, sales-admin, operations-admin")]
        public ActionResult Create([Bind(Include = "ProductId,ProviderId,Provider,Name,ProductFamily,ProviderPrice,Currency,AdditionalData,ImageURL")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "admin, management, sales-admin, operations-admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, management, sales-admin, operations-admin")]
        public ActionResult Edit([Bind(Include = "ProductId,ProviderId,Provider,Name,ProductFamily,ProviderPrice,Currency,AdditionalData,ImageURL")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (ModelState["ProviderPrice"].Errors.Count > 0)
            {
                ModelState["ProviderPrice"].Errors.Clear();
                ModelState["ProviderPrice"].Errors.Add("El precio no es válido");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "admin, management, sales-admin, operations-admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize(Roles = "admin, management, sales-admin, operations-admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
