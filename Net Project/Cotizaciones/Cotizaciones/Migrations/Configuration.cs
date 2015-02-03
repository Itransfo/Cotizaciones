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
            context.Steps.AddOrUpdate(s => s.Name,
                new Cotizaciones.Models.Step
                {
                    Order = 1,
                    Name = "Recibir la solicitud",
                    Value = 1,
                }
                , new Cotizaciones.Models.Step
                {
                    Order = 2,
                    Name = "Contactar al cliente",
                    Value = 2,
                }
                ,new Cotizaciones.Models.Step
                {
                    Order = 3,
                    Name = "Clasificar al cliente",
                    Value = 3,
                }
                , new Cotizaciones.Models.Step
                {
                    Order = 4,
                    Name = "Identificar la necesidad del cliente",
                    Value = 4,
                }
                , new Cotizaciones.Models.Step
                {
                    Order = 5,
                    Name = "Elaborar la propuesta técnica de operaciones",
                    Value = 5,
                }
                , new Cotizaciones.Models.Step
                {
                    Order = 6,
                    Name = "Elaborar la propuesta de logística",
                    Value = 6,
                }
                , new Cotizaciones.Models.Step
                {
                    Order = 7,
                    Name = "Elaborar la propuesta de finanzas",
                    Value = 7,
                }
                , new Cotizaciones.Models.Step
                {
                    Order = 8,
                    Name = "Elaborar la propuesta comercial",
                    Value = 8,
                }
                , new Cotizaciones.Models.Step
                {
                    Order = 9,
                    Name = "Entregar la cotización",
                    Value = 9,
                }
            );
            context.Clients.AddOrUpdate(p => p.Name,
                new Client
                {
                    Name = "Cliente",
                    LastName = "Ejemplo",
                    Company = "Itransfo",
                    City = "Cuernavaca",
                    State = "Morelos",
                    Country = "Mexico",
                    Category = "Homero Simpson",
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
