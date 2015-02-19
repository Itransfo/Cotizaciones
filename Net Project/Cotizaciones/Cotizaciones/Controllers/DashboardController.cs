using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cotizaciones.Models;
using System.Web.Security;

namespace Cotizaciones.Controllers
{
    [Authorize(Roles = "authorized-user")]
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public Order fillData(int id)
        {
            var orderQuery = from o in db.Orders.Include("Step") where (o.OrderId.Equals(id)) select o;
            Order order = orderQuery.ToList().ElementAt(0);
            ViewBag.OrderProducts = db.OrderProducts.ToList().Where(op => op.Order.OrderId == order.OrderId);
            ViewBag.StepChanges = order.StepChanges.ToList();
            var stepQuery = from s in db.Steps select s;
            List<Step> steps = stepQuery.ToList();
            Dictionary<int, string> stepNames = new Dictionary<int, string>();
            foreach (Step s in steps)
                stepNames.Add(s.StepId, s.getDisplayName());
            ViewBag.StepNames = stepNames;
            ViewBag.OrderComments = order.OrderComments.ToList();
            return order;
        }
                
        public ActionResult MyOrders(string sortOrder)
        {
            ViewBag.SortOrder = String.IsNullOrEmpty(sortOrder) ? "dateAsc" : sortOrder;
            var orders = from o in db.Orders.Include("Step") select o;
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
                case "stepDesc":
                    orders = orders.OrderByDescending(o => o.Step.Order);
                    break;
                case "stepAsc":
                    orders = orders.OrderBy(o => o.Step.Order);
                    break;
                case "timeDesc":
                    orders = orders.OrderByDescending(o => o.DateCreated);
                    break;
                case "timeAsc":
                    orders = orders.OrderBy(o => o.DateCreated);
                    break;
                default:
                    orders = orders.OrderBy(o => o.DateCreated);
                    break;
            }
            return View(orders.ToList());
        }

        public ActionResult FillOrder(int id)
        {            
            return View(fillData(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PushOrder(int orderId)
        {
            //User is logged in
            if(!User.Identity.IsAuthenticated || !User.IsInRole("authorized-user"))
            {
                return RedirectToAction("MyOrders");
            }
            var orderQuery = from o in db.Orders.Include("Step") where (o.OrderId.Equals(orderId)) select o;
            Order order = orderQuery.ToList().ElementAt(0);
            //User is responsible for this step
            if(!User.IsInRole(order.Step.Responsible))
            {
                return RedirectToAction("MyOrders");
            }
            //TODO: Check user has properly filled the data corresponding to this step
            //Move to next step, if succesfull, record the change
            Step previousStep = order.Step;
            var stepQuery = from s in db.Steps where (s.Order.Equals(previousStep.Order+1)) select s;
            Step newStep = stepQuery.ToList().ElementAt(0);
            try
            {
                order.Step = newStep;
                db.SaveChanges();
                StepChange stepChange = new StepChange();
                stepChange.DateChanged = DateTime.Now;
                stepChange.PreviousStepId = previousStep.StepId;
                stepChange.NextStepId = newStep.StepId;
                stepChange.User = User.Identity.Name;
                order.StepChanges.Add(stepChange);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("MyOrders");
            }
            return RedirectToAction("MyOrders");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveComment(int orderId, string comment)
        {
            //User is logged in
            if (!User.Identity.IsAuthenticated || !User.IsInRole("authorized-user"))
            {
                return RedirectToAction("MyOrders");
            }
            //User is responsible for this step
            Order order = fillData(orderId);
            if (!User.IsInRole(order.Step.Responsible))
            {
                return RedirectToAction("MyOrders");
            }
            //Move to next step, if succesfull, record the change
            try
            {
                OrderComment orderComment = new OrderComment();
                orderComment.Date = DateTime.Now;
                orderComment.Comment = comment;
                orderComment.Step = order.Step;
                orderComment.User = User.Identity.Name;
                order.OrderComments.Add(orderComment);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("FillOrder", new { @id = orderId });
            }
            return RedirectToAction("FillOrder", new { @id = orderId });
        }

    }
}