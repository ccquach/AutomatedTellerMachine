namespace AutomatedTellerMachine.Migrations
{
    using AutomatedTellerMachine.Models;
    using AutomatedTellerMachine.Services;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomatedTellerMachine.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AutomatedTellerMachine.Models.ApplicationDbContext";
        }

        protected override void Seed(AutomatedTellerMachine.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "admin@mvcatm.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@mvcatm.com",
                    Email = "admin@mvcatm.com"
                };
                IdentityResult result = userManager.Create(user, "Password1!");

                var service = new CheckingAccountService(context);
                service.CreateCheckingAccount("admin", "user", user.Id, 1000);

                context.Roles.AddOrUpdate(t => t.Name, new IdentityRole { Name = "Admin" });
                context.SaveChanges();
                //SaveChanges(context);

                userManager.AddToRole(user.Id, "Admin");
                context.SaveChanges();
            }

            context.Transactions.Add(new Transaction { Amount = 75, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = -25, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 100000, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 19.99m, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 64.40m, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 100, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = -300, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 255.75m, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 198, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 2, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 50, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = -1.50m, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 6100, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 164.84m, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = .01m, CheckingAccountId = 5 });

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
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            //catch (DbEntityValidationException ex)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    foreach (var failure in ex.EntityValidationErrors)
            //    {
            //        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
            //        foreach (var error in failure.ValidationErrors)
            //        {
            //            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
            //            sb.AppendLine();
            //        }
            //    }

            //    throw new DbEntityValidationException(
            //        "Entity Validation Failed - errors follow:\n" +
            //        sb.ToString(), ex
            //    ); // Add the original exception as the innerException
            //}
        }
    }
}
