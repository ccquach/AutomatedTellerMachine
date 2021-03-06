﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedTellerMachine.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AutomatedTellerMachine.Models;

namespace AutomatedTellerMachine.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /home/index/
        [Authorize]
        public ActionResult Index()
        {
            // Get checking account Id associated with logged-in user
            var userId = User.Identity.GetUserId();
            var checkingAccountId = db.CheckingAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
            ViewBag.CheckingAccountId = checkingAccountId;

            // Get Pin associated with logged-in user
            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(userId);
            ViewBag.Pin = user.Pin;

            return View();
        }

        // GET: /home/about/
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }

        // GET: /home/contact/
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.TheMessage = "Having trouble? Send us a message.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            // TODO: send message to HQ
            ViewBag.TheMessage = "Thanks, we got your message!";
            return PartialView("_ContactThanks");
        }

        public ActionResult Foo()
        {
            return View("About");
        }

        [Route("serial")]
        public ActionResult Serial(string letterCase)
        {
            var serial = "ASPNETMVC5ATM1";
            if (letterCase == "lower")
            {
                return Content(serial.ToLower());
            }
            return Content(serial);
            //return new HttpStatusCodeResult(403);
            //return Json(new { name = "serial", value = serial }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index");
        }
    }
}