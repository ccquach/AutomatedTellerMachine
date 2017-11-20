﻿using System;
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
            if (ModelState.IsValid)
            {
                // Add transaction to db
                db.Transactions.Add(transaction);
                db.SaveChanges();

                // Update checking account balance
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: Transaction/Withdrawal
        public ActionResult Withdrawal(int checkingAccountId)
        {
            return View();
        }

        // POST: Transaction/Withdrawal
        [HttpPost]
        public ActionResult Withdrawal(Transaction transaction)
        {
            var checkingAccount = db.CheckingAccounts.Find(transaction.CheckingAccountId);
            if (transaction.Amount > checkingAccount.Balance)
            {
                ModelState.AddModelError("Amount", "You have insufficient funds!");
            }

            if (ModelState.IsValid)
            {
                // Add transaction to db
                transaction.Amount = -transaction.Amount;
                db.Transactions.Add(transaction);
                db.SaveChanges();

                // Update checking account balance
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: Transaction/Transfer
        public ActionResult Transfer(int checkingAccountId)
        {
            ViewBag.TransferConfirmationMessage = "";
            return View();
        }

        // POST: Transaction/Transfer
        [HttpPost]
        public ActionResult Transfer(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                ViewBag.TransferConfirmationMessage = "Fund transfer successful!";
                
            }
            else
            {
                ViewBag.TransferConfirmationMessage = "Fund transfer unsuccesful!";
            }
            return PartialView("_TransferConfirmation");
        }
    }
}
