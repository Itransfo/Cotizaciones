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
            //context.Clients.AddOrUpdate(p => p.Name,
            //new Client
            //{
            //    Name = "Juan Perez",
            //    City = "D.F.",
            //    Email = "jperez@ejemplo.com",
            //},
            // new Client
            // {
            //     Name = "José Franco",
            //     City = "Cuernavaca",
            //     Email = "jose.franco@outlook.com",
            // }
            //);
        }

        bool AddUserAndRole(Cotizaciones.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("admin"));
            ir = rm.Create(new IdentityRole("user"));
            ir = rm.Create(new IdentityRole("sales-admin"));
            ir = rm.Create(new IdentityRole("sales-user"));
            ir = rm.Create(new IdentityRole("logistics-admin"));
            ir = rm.Create(new IdentityRole("logistics-user"));
            ir = rm.Create(new IdentityRole("finance-admin"));
            ir = rm.Create(new IdentityRole("finance-user"));
            ir = rm.Create(new IdentityRole("operations-admin"));
            ir = rm.Create(new IdentityRole("operations-user"));
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
