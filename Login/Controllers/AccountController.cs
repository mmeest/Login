using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Membership model)
        {
            return View();
        }
        // GET: Account
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            using(var context = new OfficeEntities())
            {
                context.User.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("login");
        }
    }
}