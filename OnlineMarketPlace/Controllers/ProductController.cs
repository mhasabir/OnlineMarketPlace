using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Data;

namespace OnlineMarketPlace.Controllers
{
    public class ProductController : Controller
    {

        //DataTable cartData = new DataTable("CartHistory");

        public ActionResult ProductReport(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            Product p = context.Products.SingleOrDefault(d => d.Id == id);
            ViewBag.totalSold = p.sold;
            ViewBag.quantity = p.Quantity;
            ViewBag.totalSoldPrice = p.sold * p.Price;

            int a = 0;
            List<string> userlist = new List<string>();
            for (int i = 0; i < p.sold; i++)
            {
                UserProduct up = context.UserProducts.FirstOrDefault(d => d.ProductId == id && a < d.Id);
                User u = context.Users.SingleOrDefault(d => d.Id == up.UserId);
                userlist.Add(u.Name);

            }
            ViewBag.userlist = userlist;
            return View();
        }
        

        public ActionResult List()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            List<Product> plist = context.Products.ToList();
            return View(plist);
        }
        [HttpGet]
        public ActionResult UserProductList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            List<Product> uplist = context.Products.ToList();
            return View(uplist);
        }
        [HttpGet]
        public ActionResult UserProductList1(Product emp)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            List<Product> plist = context.Products.ToList();
            Product product = context.Products.SingleOrDefault(d => d.Id == emp.Id);
            User userAdmin = (User)Session["user"];
            User myUpdate = context.Users.SingleOrDefault(u => u.User_Name == userAdmin.User_Name && u.Password == userAdmin.Password);

            Cart ob = new Cart();
            ob.ProductId = product.Id;

            ob.UserId = myUpdate.Id;
            //addToCartList(product);
            myUpdate.cartItem++;

            context.Carts.Add(ob);
            context.SaveChanges();
            Session["user"] = myUpdate;
            TimeSpan.FromSeconds(5000000);
            return View("UserProductList",plist);
        }


        /*
        [NonAction]
        public void addToCartList(Product product)
        {
            DataRow dr = cartData.NewRow();
            cartData.Columns.Add("id", typeof(int));
            dr["id"] = product.Id;
            cartData.Rows.Add(dr);
               
       
           Session["CARTDATA"] = cartData;

        }*/

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

              ViewBag.clist = this.GetCategoryList();
                return View();
            
        }

        [HttpPost]
        public ActionResult Create(Product u)
        {
            using (OnlineDbContext context = new OnlineDbContext())
            {
                if (ModelState.IsValid)
                {
                    u.sold = 0;
                    context.Products.Add(u);
                    context.SaveChanges();
                    return View("ProductCreateComplete");

                }
                else
                {
                    return View();
                }
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
            Product product = context.Products.SingleOrDefault(d => d.Id == id);
            ViewBag.clist = this.GetCategoryList();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product emp)
        {
            if (ModelState.IsValid)
            {
                OnlineDbContext context = new OnlineDbContext();
                Product myUpdate = context.Products.SingleOrDefault(d => d.Id == emp.Id);

                myUpdate.ProductName = emp.ProductName;
                myUpdate.Picture = emp.Picture;
                myUpdate.Details = emp.Details;
                myUpdate.CategoryId = emp.CategoryId;
                myUpdate.Price = emp.Price;
                myUpdate.Quantity = emp.Quantity;

                context.SaveChanges();
                return RedirectToAction("List");

            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            OnlineDbContext context = new OnlineDbContext();
            Product delEmp = context.Products.SingleOrDefault(d => d.Id == id);
            return View(delEmp);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            OnlineDbContext context = new OnlineDbContext();
            Product myDelete = context.Products.SingleOrDefault(d => d.Id == id);
            context.Products.Remove(myDelete);
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
            Product details = context.Products.SingleOrDefault(d => d.Id == id);
            return View(details);
        }
        [NonAction]
        public List<SelectListItem> GetCategoryList()
        {
            OnlineDbContext context = new OnlineDbContext();
            List<SelectListItem> clist = new List<SelectListItem>();
            foreach (Category c in context.Categories.ToList())
            {
                SelectListItem li = new SelectListItem();
                li.Text = c.CategoryName;
                li.Value = c.Id.ToString();
                clist.Add(li);
            }
            return clist;

        }
    }
}
