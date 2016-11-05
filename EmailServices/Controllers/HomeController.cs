using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EmailServices.Models;

namespace EmailServices.Controllers
{
    public class HomeController : Controller
    {
        // GET: Account
        public ActionResult Messages()
        {
            using (MessageDb db = new MessageDb())
            {
                return View(db.message.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }

                ModelState.Clear();
                ViewBag.Message = account.FirstName + " " + account.LastName + "successfully registered.";
            }

            return View();
        }

        //Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.userAccount.Single(u => u.Username == user.Username && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }
            }

            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult LoggedIn(Message message)
        {
            if (ModelState.IsValid)
            {
                using (MessageDb db = new MessageDb())
                {
                    db.message.Add(message);
                    db.SaveChanges();
                }

                ModelState.Clear();
                ViewBag.Message = message.Receiver + " " + message.Subject + "successfully stored.";
            }

            return View();
        }
    }
}