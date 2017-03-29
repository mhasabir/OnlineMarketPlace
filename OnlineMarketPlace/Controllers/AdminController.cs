using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using DataLayer;

namespace OnlineMarketPlace.Controllers.Admin
{
    public class AdminController : Controller
    {
        public ActionResult LogOff()
        {
            Session.Remove("admin");
            return RedirectToAction("LogIn", "Home");
        }
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


        [HttpGet]
        public ActionResult UserList()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            List<User> ulist = context.Users.ToList();
            return View(ulist);
        }



        [HttpGet]
        public ActionResult Edit()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(Session["Admin"]);
        }

        [HttpPost]
        public ActionResult Edit(User emp)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                OnlineDbContext context = new OnlineDbContext();
                User userAdmin = (User) Session["Admin"];
                User myUpdate = context.Users.SingleOrDefault(u => u.User_Name == userAdmin.User_Name && u.Password == userAdmin.Password);
                myUpdate.Name = emp.Name;
                myUpdate.User_Name = emp.User_Name;
                myUpdate.Password = emp.Password;
                myUpdate.Email = emp.Email;
                myUpdate.Mobile = emp.Mobile;
                myUpdate.Location = emp.Location;

                context.SaveChanges();
                Session["Admin"] = myUpdate;
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }

        }

        public ActionResult Details()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(Session["Admin"]);
        
        }
    }
}
