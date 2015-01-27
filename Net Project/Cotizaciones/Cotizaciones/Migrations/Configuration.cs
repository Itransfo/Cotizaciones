using Cotizaciones.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cotizaciones.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Cotizaciones.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            AddUserAndRole(context);
            context.Clients.AddOrUpdate(p => p.Name,
            new Client
            {
                Name = "Debra Garcia",
                Location = "Redmond",
                Email = "debra@example.com",
            },
             new Client
             {
                 Name = "Thorsten Weinrich",
                 Location = "Redmond",
                 Email = "thorsten@example.com",
             },
             new Client
             {
                 Name = "Yuhong Li",
                 Location = "Redmond",
                 Email = "yuhong@example.com",
             }
         );
        }

        bool AddUserAndRole(Cotizaciones.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("admin"));
            ir = rm.Create(new IdentityRole("user"));
            ir = rm.Create(new IdentityRole("sales"));
            ir = rm.Create(new IdentityRole("logistics"));
            ir = rm.Create(new IdentityRole("finance"));
            ir = rm.Create(new IdentityRole("operations"));
            ir = rm.Create(new IdentityRole("management"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "jose.franco@itransfo.com",
            };
            ir = um.Create(user, "2153680");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "admin");
            return ir.Succeeded;
        }
    }
}
