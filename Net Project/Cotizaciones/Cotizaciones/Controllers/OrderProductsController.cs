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
    public class OrderProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderProducts
        public ActionResult Index()
        {
            return View(db.OrderProducts.ToList());
        }

        // GET: OrderProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return HttpNotFound();
            }
            return View(orderProduct);
        }

        // GET: OrderProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderProductId,Quantity")] OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                db.OrderProducts.Add(orderProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderProduct);
        }

        // GET: OrderProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return HttpNotFound();
            }
            return View(orderProduct);
        }

        // POST: OrderProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderProductId,Quantity")] OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderProduct);
        }

        // GET: OrderProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return HttpNotFound();
            }
            return View(orderProduct);
        }

        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            db.OrderProducts.Remove(orderProduct);
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
