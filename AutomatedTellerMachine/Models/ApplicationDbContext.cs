using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<CheckingAccount> CheckingAccounts { get; set; }
        IDbSet<Transaction> Transactions { get; set; }

        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<CheckingAccount> CheckingAccounts { get; set; }
        public IDbSet<Transaction> Transactions { get; set; }

        // Get details of EntityValidationErrors
        public override int SaveChanges()
        {
            try
            {
                base.SaveChanges();
                return 0;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception exception = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);

                        //create a new exception inserting the current one
                        //as the InnerException
                        exception = new InvalidOperationException(message, exception);
                    }
                }
                throw exception;
            }
        }
    }

    public class FakeApplicationDbContext : IApplicationDbContext
    {
        public IDbSet<CheckingAccount> CheckingAccounts { get; set; }
        public IDbSet<Transaction> Transactions { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}