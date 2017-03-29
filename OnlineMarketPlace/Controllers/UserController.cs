using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Data;

namespace OnlineMarketPlace.Controllers
{
    public class UserController : Controller
    {
        public ActionResult LogOff()
        {
            Session.Remove("user");
            return RedirectToAction("LogIn", "Home");
        }
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpGet]
        public ActionResult UserCart()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(getUserCart());
        }
        public List<Product> getUserCart()
        {
        
            using (OnlineDbContext context = new OnlineDbContext())
            {   /*
                //DataTable cartTemp = new DataTable();

               // cartTemp = (DataTable)Session["CARTDATA"];

                List<Product> plist = new List<Product>();

               // if (cartTemp.Rows.Count != 0)
              // {
               //     for (int i = 0; i < cartTemp.Rows.Count;i++)
               //     {
                        Product product = context.Products.SingleOrDefault(d => d.Id ==Convert.ToInt32(cartTemp.Rows[i]["id"].ToString()));
                        plist.Add(product);
                //    }

                    return View(plist);
              // }
             // else
              //      return View();*/

                int a = 0;
                List<Product> plist = new List<Product>();
                User ob=(User)Session["user"];
                User normalUser = context.Users.SingleOrDefault(u => u.User_Name == ob.User_Name && u.Password == ob.Password);
                for (int i = 0; i < normalUser.cartItem; i++)
                {

                    Cart cartobj = context.Carts.FirstOrDefault(d => d.UserId == normalUser.Id && d.Id>a);
                    
                    a = cartobj.Id;
               
                    Product product = context.Products.SingleOrDefault(d => d.Id == cartobj.ProductId);
                    plist.Add(product);
                //    }

              
                }
           
                return plist;
            }
        }
        public ActionResult PurchasedProduct()
        {

            return View(getPurchasedProduct());
        
        }
        public List<Product> getPurchasedProduct()
        {
            using (OnlineDbContext context = new OnlineDbContext())
            {   

                int a = 0;
                List<Product> plist = new List<Product>();
                User ob = (User)Session["user"];
                User normalUser = context.Users.SingleOrDefault(u => u.User_Name == ob.User_Name && u.Password == ob.Password);
                for (int i = 0; i < normalUser.purchasedItem; i++)
                {

                    UserProduct cartobj = context.UserProducts.FirstOrDefault(d => d.UserId == normalUser.Id && d.Id > a);

                    a = cartobj.Id;

                    Product product = context.Products.SingleOrDefault(d => d.Id == cartobj.ProductId);
                    plist.Add(product);
                    //    }


                }

                return plist;
            }
        }
        public ActionResult RemoveFromCart(int id)
        {
            OnlineDbContext context = new OnlineDbContext();
            Cart cartobj = context.Carts.FirstOrDefault(d => d.ProductId ==id);
            context.Carts.Remove(cartobj);

            User userAdmin = (User)Session["user"];
            User myUpdate = context.Users.SingleOrDefault(u => u.User_Name == userAdmin.User_Name && u.Password == userAdmin.Password);
            myUpdate.cartItem--;

            context.SaveChanges();
            Session["user"] = myUpdate;

            return View("UserCart", getUserCart());
        
        }
        public ActionResult CheckOut(int id)
        {
            OnlineDbContext context = new OnlineDbContext();
            Cart cartobj = context.Carts.FirstOrDefault(d => d.ProductId ==id);
            context.Carts.Remove(cartobj);

            User userAdmin = (User)Session["user"];
            User myUpdate = context.Users.SingleOrDefault(u => u.User_Name == userAdmin.User_Name && u.Password == userAdmin.Password);
            myUpdate.cartItem--;
            Product pr = context.Products.SingleOrDefault(d => d.Id == id);
            pr.Quantity--;
            pr.sold++;

            UserProduct up = new UserProduct();
            up.ProductId = id;
            up.UserId = myUpdate.Id;

            context.UserProducts.Add(up);
            myUpdate.purchasedItem++;

            context.SaveChanges();
            Session["user"] = myUpdate;


            return View("UserCart", getUserCart());
        
        }
        
        [HttpGet]
        public ActionResult Edit()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(Session["user"]);
        }

        [HttpPost]
        public ActionResult Edit(User emp)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                OnlineDbContext context = new OnlineDbContext();
                User normalUser = (User)Session["user"];
                User myUpdate = context.Users.SingleOrDefault(u => u.User_Name == normalUser.User_Name && u.Password == normalUser.Password);
                myUpdate.Name = emp.Name;
                myUpdate.User_Name = emp.User_Name;
                myUpdate.Password = emp.Password;
                myUpdate.Email = emp.Email;
                myUpdate.Mobile = emp.Mobile;
                myUpdate.Location = emp.Location;

                context.SaveChanges();
                Session["user"] = myUpdate;
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }

        }

        public ActionResult Details()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(Session["user"]);

        }
    }
}
