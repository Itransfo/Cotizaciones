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
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = String.IsNullOrEmpty(sortOrder) ? "nameAsc" : sortOrder;
            var clients = from c in db.Clients select c;
            switch (sortOrder)
            {
                case "nameDesc":
                    clients = clients.OrderByDescending(c => c.Name);
                    break;
                case "categoryDesc":
                    clients = clients.OrderByDescending(c => c.Category);
                    break;
                case "categoryAsc":
                    clients = clients.OrderBy(c => c.Category);
                    break;
                default:
                    clients = clients.OrderBy(c => c.Name);
                    break;
            }
            return View(clients);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [Authorize(Roles = "admin, management, sales-admin")]
        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin, management, sales-admin")]
        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,Name,LastName,Company,Address,City,State,Country,Phone,Extension,CellPhone,Email,Category")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [Authorize(Roles = "admin, management, sales-admin")]
        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [Authorize(Roles = "admin, management, sales-admin")]
        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,Name,LastName,Company,Address,City,State,Country,Phone,Extension,CellPhone,Email,Category")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [Authorize(Roles = "admin, management, sales-admin")]
        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [Authorize(Roles = "admin, management, sales-admin")]
        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
