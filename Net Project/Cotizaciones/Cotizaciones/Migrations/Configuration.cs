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
            try
            {
                AddUserAndRole(context);
                context.Steps.AddOrUpdate(s => s.Name,
                    new Cotizaciones.Models.Step
                    {
                        Order = 1,
                        Name = "Recibir la solicitud",
                        Value = 1,
                        Responsible = "sales-admin",
                        Tolerance = 48,
                        Reminder = 24,
                    }
                    , new Cotizaciones.Models.Step
                    {
                        Order = 2,
                        Name = "Contactar al cliente",
                        Value = 2,
                        Responsible = "sales-admin",
                        Tolerance = 48,
                        Reminder = 24,
                    }
                    , new Cotizaciones.Models.Step
                    {
                        Order = 3,
                        Name = "Clasificar al cliente",
                        Value = 3,
                        Responsible = "sales-admin",
                        Tolerance = 48,
                        Reminder = 24,
                    }
                    , new Cotizaciones.Models.Step
                    {
                        Order = 4,
                        Name = "Identificar la necesidad del cliente",
                        Value = 4,
                        Responsible = "sales-admin",
                        Tolerance = 48,
                        Reminder = 24,
                    }
                    , new Cotizaciones.Models.Step
                    {
                        Order = 5,
                        Name = "Elaborar la propuesta técnica de operaciones",
                        Value = 5,
                        Responsible = "operations-admin",
                        Tolerance = 48,
                        Reminder = 24,
                    }
                    , new Cotizaciones.Models.Step
                    {
                        Order = 6,
                        Name = "Elaborar la propuesta de logística",
                        Value = 6,
                        Responsible = "logistics-admin",
                        Tolerance = 48,
                        Reminder = 24,
                    }
                    , new Cotizaciones.Models.Step
                    {
                        Order = 7,
                        Name = "Elaborar la propuesta de finanzas",
                        Value = 7,
                        Responsible = "finance-admin",
                        Tolerance = 48,
                        Reminder = 24,
                    }
                    , new Cotizaciones.Models.Step
                    {
                        Order = 8,
                        Name = "Elaborar la propuesta comercial",
                        Value = 8,
                        Responsible = "sales-admin",
                        Tolerance = 48,
                        Reminder = 24,
                    }
                    , new Cotizaciones.Models.Step
                    {
                        Order = 9,
                        Name = "Entregar la cotización",
                        Value = 9,
                        Responsible = "sales-admin",
                        Tolerance = 48,
                        Reminder = 24,
                    }
                    , new Cotizaciones.Models.Step
                    {
                        Order = 99,
                        Name = "Standby",
                        Value = 0,
                        Responsible = "none",
                        Tolerance = int.MaxValue,
                        Reminder = int.MaxValue,
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
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }  
            
        }

        bool AddUserAndRole(Cotizaciones.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("admin"));
            ir = rm.Create(new IdentityRole("user"));
            ir = rm.Create(new IdentityRole("authorized-user"));
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
            ir = um.Create(user, "itransfo2015");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "admin");
            ir = um.AddToRole(user.Id, "authorized-user");
            return ir.Succeeded;
        }
    }
}
