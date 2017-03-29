using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace OnlineMarketPlace.Controllers
{
    public class CategoryController : Controller
    {

        public ActionResult List()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            List<Category> clist = context.Categories.ToList();
            return View(clist);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category u)
        {
            using (OnlineDbContext context = new OnlineDbContext())

                if (ModelState.IsValid)
                {
                    context.Categories.Add(u);
                    context.SaveChanges();
                    return View("CategoryCreateComplete");

                }
                else
                {
                    return View();
                }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            Category category = context.Categories.SingleOrDefault(d => d.Id == id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category emp)
        {
            if (ModelState.IsValid)
            {
                OnlineDbContext context = new OnlineDbContext();
                Category myUpdate = context.Categories.SingleOrDefault(d => d.Id == emp.Id);

                myUpdate.CategoryName = emp.CategoryName;

                context.SaveChanges();
                return RedirectToAction("List");

            }
            else
            {
                return View();
            }

        }
        public ActionResult Delete(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            Category delEmp = context.Categories.SingleOrDefault(d => d.Id == id);
            return View(delEmp);
        }

        [HttpPost][ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            OnlineDbContext context = new OnlineDbContext();
            Category myDelete = context.Categories.SingleOrDefault(d => d.Id == id);
            context.Categories.Remove(myDelete);
            context.SaveChanges();

            return RedirectToAction("List");
        } 

        public ActionResult Details(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            Category details = context.Categories.SingleOrDefault(d => d.Id == id);
            return View(details);
        }
    }
}
