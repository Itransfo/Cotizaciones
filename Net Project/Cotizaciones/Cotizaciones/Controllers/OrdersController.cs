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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = String.IsNullOrEmpty(sortOrder) ? "dateAsc" : sortOrder;
            var orders = from o in db.Orders select o;
            switch (sortOrder)
            {
                case "dateDesc":
                    orders = orders.OrderByDescending(o => o.DateCreated);
                    break;
                case "identifierDesc":
                    orders = orders.OrderByDescending(o => o.Identifier);
                    break;
                case "identifierAsc":
                    orders = orders.OrderBy(o => o.Identifier);
                    break;
                case "projectDesc":
                    orders = orders.OrderByDescending(o => o.Preproject);
                    break;
                case "projectAsc":
                    orders = orders.OrderBy(o => o.Preproject);
                    break;
                default:
                    orders = orders.OrderBy(o => o.DateCreated);
                    break;
            }
            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.StepChanges = order.StepChanges.ToList();
            ViewBag.OrderProducts = db.OrderProducts.ToList().Where(op => op.Order.OrderId == order.OrderId);
            var stepQuery = from s in db.Steps select s;
            List<Step> steps = stepQuery.ToList();
            Dictionary<int, string> stepNames = new Dictionary<int, string>();
            foreach (Step s in steps)
                stepNames.Add(s.StepId, s.getDisplayName());
            ViewBag.StepNames = stepNames;
            ViewBag.OrderComments = order.OrderComments.ToList();
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "admin, management, sales-admin")]
        public ActionResult Create()
        {
            ViewBag.Clients = db.Clients.ToList();
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, management, sales-admin")]
        public ActionResult Create([Bind(Include = "OrderId,Identifier,Preproject,RequestDescription,Client")] Order order, int clientId)
        {
            if (ModelState.IsValid)
            {
                if(db.Orders.Select(o => o.Identifier).ToList().Contains(order.Identifier))
                {
                    ModelState.AddModelError("Identifier", "Ya existe una orden con ese identificador");
                    return View();
                }
                order.DateCreated = DateTime.Now;
                var steps = from s in db.Steps where s.Order == 1 select s;
                order.Step = steps.ToList().ElementAt(0);
                var clients = from s in db.Clients where s.ClientId == clientId select s;
                order.Client = clients.ToList().ElementAt(0);
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "admin, management, sales-admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Clients = db.Clients.ToList();
            ViewBag.Products = db.Products.ToList();
            ViewBag.OrderProducts = db.OrderProducts.ToList().Where(op => op.Order.OrderId == order.OrderId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, management, sales-admin")]
        public ActionResult Edit([Bind(Include = "OrderId,Identifier,Preproject,RequestDescription")] Order order, int clientId)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(order).State = EntityState.Modified;
                Order toModify = db.Orders.Find(order.OrderId);
                toModify.Identifier = order.Identifier;
                toModify.Preproject = order.Preproject;
                toModify.RequestDescription = order.RequestDescription;
                var clients = from s in db.Clients where s.ClientId == clientId select s;
                toModify.Client = clients.ToList().ElementAt(0);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "admin, management, sales-admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, management, sales-admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        [HttpPost]
        [Authorize(Roles = "admin, management, sales-admin")]
        public ActionResult AddOrderProduct(int orderId, int productId, string quantity)
        {
            int number = 0;
            Order order = db.Orders.Find(orderId);
            try
            {
                number = int.Parse(quantity);
                Product product = db.Products.Find(productId);
                OrderProduct orderProduct = new OrderProduct();
                orderProduct.Order = order;
                orderProduct.Product = product;
                orderProduct.Quantity = number;
                db.OrderProducts.Add(orderProduct);
                db.SaveChanges();
            }
            catch
            {
                
            }
            ViewBag.Clients = db.Clients.ToList();
            ViewBag.Products = db.Products.ToList();
            ViewBag.OrderProducts = db.OrderProducts.ToList().Where(op => op.Order.OrderId == order.OrderId);
            return RedirectToAction("Edit", new { @id = order.OrderId });
        }

        [Authorize(Roles = "admin, management, sales-admin")]
        public ActionResult DeleteOrderProduct(int orderProductId)
        {
            OrderProduct orderProduct = db.OrderProducts.Find(orderProductId);
            int orderId = orderProduct.Order.OrderId;
            ViewBag.Clients = db.Clients.ToList();
            ViewBag.Products = db.Products.ToList();
            ViewBag.OrderProducts = db.OrderProducts.ToList().Where(op => op.Order.OrderId == orderId);
            db.OrderProducts.Remove(orderProduct);
            db.SaveChanges();
            return RedirectToAction("Edit", new { @id = orderId });
        }
    }
}
