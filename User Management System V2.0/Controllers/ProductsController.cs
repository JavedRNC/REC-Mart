using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using User_Management_System_V2._0.Models;
using WebMatrix.Data;
using WebMatrix.WebData;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Management_System_V2._0.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private UserManagementEntities db = new UserManagementEntities();

        // GET: Products


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]


        public ActionResult Create([Bind(Include = "ProductID,ProductName,Description,PriceExpected,OldTime,Status,Photo,PostedBy,Date")] Product product, HttpPostedFileBase file)
        {
            if (WebSecurity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        product.Photo = new byte[file.ContentLength];
                        file.InputStream.Read(product.Photo, 0, file.ContentLength);
                    }

                    product.PostedBy = WebSecurity.CurrentUserName;
                    product.Date = DateTime.Now;
                    product.Status = -1;

                    db.Products.Add(product);
                    db.SaveChanges();



                    return RedirectToAction("RequestedToPost");
                }
            }

            else
            {
                ModelState.AddModelError("", "You need to be logged in ! Please login and then Add Product !");
            }

            return View(product);
        }

        public ActionResult YourAds()
        {
            List<Product> products = db.Products.Where(x => x.PostedBy == WebSecurity.CurrentUserName).ToList();

            return View(products);
        }


        public ActionResult RequestedToPost()
        {
            return View();
        }

        public ActionResult YourRequestedItems()
        {
            List<Product> products = db.Products.Where(x => x.RequestedBy == WebSecurity.CurrentUserName && x.Status == 1).ToList();
            return View(products);
        }

        [Authorize(Users ="admin33")]
        public ActionResult Edit(int? id)
        {
            Product product = db.Products.Find(id);

            if (product.PostedBy == WebSecurity.CurrentUserName)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }

                if (product == null)
                {
                    return HttpNotFound();
                }


                return View(product);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin33")]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    product.Photo = new byte[file.ContentLength];
                    file.InputStream.Read(product.Photo, 0, file.ContentLength);
                    product.Status = -1;
                    product.PostedBy = WebSecurity.CurrentUserName;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("RequestedToPost");
                }
                else
                {
                    ModelState.AddModelError("", errorMessage: " Image is null ");
                }

            }
            return View(product);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            Product product = db.Products.Find(id);

            if (product.PostedBy == WebSecurity.CurrentUserName)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

        }


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("YourAds");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult RequestItem(int id)
        {
            Product product = db.Products.Single(x => x.ProductID == id);
            if (product.Status == 1)
            {
                return RedirectToAction("ItemNotAvailable", "Products");
            }
            else
            {
                if (WebSecurity.CurrentUserName != product.PostedBy)
                {
                    product.Status = 1;
                    product.RequestedBy = WebSecurity.CurrentUserName;
                    db.SaveChanges();
                }

                    return View(product);
                }


            }
            public ActionResult ItemNotAvailable()
            {
                return View();
            }

            public ActionResult LatestSoldOut()
            {
                List<Product> products = db.Products.Where(x => x.Status == 2).ToList();
                return View(products);
            }

            public ActionResult UnRequested(int id)
            {
                Product product = db.Products.Single(x => x.ProductID == id);
                product.Status = 0;
                db.SaveChanges();

                return RedirectToAction("YourRequesteditems", "Products");
            }

            public ActionResult Search(string SearchQuery)
            {
                List<Product> products = db.Products.Where(x => x.ProductName.Contains(SearchQuery)).ToList();

                return View(products);
            }
        }
    }
