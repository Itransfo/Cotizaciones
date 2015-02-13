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

        private ApplicationDbContext context = new ApplicationDbContext();

        public void fillData(UserManager<ApplicationUser> userManager)
        {
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            List<string> userRoles = new List<string>();
            List<ApplicationUser> users = context.Users.ToList();
            foreach (ApplicationUser user in users)
            {
                if(userManager.GetRoles(user.Id).Count == 0)
                    userRoles.Add(user.UserName + " - NO ROLES");
                foreach (string role in userManager.GetRoles(user.Id))
                    userRoles.Add(user.UserName + " - " + role);
            }
            ViewBag.UserRoles = userRoles;
            List<string> stringUserNames = users.Select(user => user.UserName).ToList();
            stringUserNames = stringUserNames.OrderBy(s => s).ToList();
            SelectList userNames = new SelectList(stringUserNames);
            ViewBag.Users = userNames;
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
            var roles = context.Roles.ToList();
            return View(roles);
        }
        
        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(thisRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
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
            fillData(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userValidator = new UserValidator<ApplicationUser>(userManager);
            userValidator.AllowOnlyAlphanumericUserNames = false;
            userManager.UserValidator = userValidator;
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var account = new AccountController();
            var result = userManager.AddToRole(user.Id, RoleName);
            ViewBag.ResultMessage = "Role created successfully !";
            return RedirectToAction("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userValidator = new UserValidator<ApplicationUser>(userManager);
            userValidator.AllowOnlyAlphanumericUserNames = false;
            userManager.UserValidator = userValidator;
            var account = new AccountController();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            try
            {
                var result = userManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            catch
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            return RedirectToAction("ManageUserRoles");
        }
    }
}