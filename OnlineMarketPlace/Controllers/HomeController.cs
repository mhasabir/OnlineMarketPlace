using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;


namespace OnlineMarketPlace.Controllers.Home
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            using (OnlineDbContext context = new OnlineDbContext())
            {
                return View(context.Users.ToList());
            }
     

        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistrationComplete()
        {
            return View();
        }

        [HttpPost][ActionName ("Registration")]
        public ActionResult SignUp(User u)
        {
            using (OnlineDbContext context = new OnlineDbContext())
            
            if (ModelState.IsValid)
            {
                u.cartItem = 0;
                u.IsAdmin = 0;
                u.purchasedItem = 0;
                context.Users.Add(u);
                context.SaveChanges();
                return View("RegistrationComplete");

            }
            else
            {
                return View();
            }

        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult LoginAction(User login)
        {

            using (OnlineDbContext context = new OnlineDbContext())
            {

                User LoggedUser = context.Users.SingleOrDefault(u => u.User_Name == login.User_Name && u.Password == login.Password);


                if (LoggedUser != null)
                {
                 
                    if (LoggedUser.IsAdmin == 0)
                    {
                        Session["user"] = LoggedUser;

                        return RedirectToAction("Index", "User");
                    }


                    else if (LoggedUser.IsAdmin == 1)
                    {
                        Session["admin"] = LoggedUser;
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
          
            ViewBag.Message = "Invalid Username or Password";
            return View("Login");
        }
    }
}

