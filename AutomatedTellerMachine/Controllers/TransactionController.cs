﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutomatedTellerMachine.Models;

namespace AutomatedTellerMachine.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transaction/Deposit
        public ActionResult Deposit(int checkingAccountId)
        {
            if (System.Web.HttpContext.Current.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Account");
            }
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
