using System;
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
        // GET: Transaction/Deposit
        public ActionResult Deposit()
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
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
