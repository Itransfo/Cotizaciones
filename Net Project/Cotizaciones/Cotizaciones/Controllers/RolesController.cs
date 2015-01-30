using Cotizaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cotizaciones.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {

        public void fillData()
        {
            var context = new ApplicationDbContext();
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            List<string> userRoles = new List<string>();
            List<ApplicationUser> users = context.Users.ToList();
            foreach (ApplicationUser user in users)
            {
                if(userManager.GetRoles(user.Id).Count == 0)
                    userRoles.Add(user.UserName + " - NO ROLES");
                foreach (string role in userManager.GetRoles(user.Id))
                    userRoles.Add(user.UserName + " - " + role);
            }
            List<string> stringUserNames = users.Select(user => user.UserName).ToList();
            stringUserNames = stringUserNames.OrderBy(s => s).ToList();
            SelectList userNames = new SelectList(stringUserNames);
            ViewBag.Users = userNames;
            ViewBag.UserRoles = userRoles;
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var roles = context.Roles.ToList();
            return View(roles);
        }
        
        public ActionResult Delete(string RoleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string roleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(thisRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            fillData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var account = new AccountController();
            userManager.AddToRole(user.Id, RoleName);
            ViewBag.ResultMessage = "Role created successfully !";
            fillData();
            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var account = new AccountController();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            try
            {
                userManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            catch
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            fillData();
            return View("ManageUserRoles");
        }
    }
}