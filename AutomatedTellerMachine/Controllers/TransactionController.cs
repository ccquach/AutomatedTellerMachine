using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedTellerMachine.Models;
using AutomatedTellerMachine.Services;

namespace AutomatedTellerMachine.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private IApplicationDbContext db;

        public TransactionController()
        {
            db = new ApplicationDbContext();
        }

        public TransactionController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: Transaction/Deposit
        public ActionResult Deposit(int checkingAccountId)
        {
            return View();
        }

        // POST: Transaction/Deposit
        [HttpPost]
        public ActionResult Deposit(Transaction transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Transactions.Add(transaction);
                    db.SaveChanges();

                    var service = new CheckingAccountService(db);
                    service.UpdateBalance(transaction.CheckingAccountId);

                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
