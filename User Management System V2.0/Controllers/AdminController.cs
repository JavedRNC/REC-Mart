using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using User_Management_System_V2._0.Models;

namespace User_Management_System_V2._0.Controllers
{
    [Authorize(Users = "admin33")]
    public class AdminController : Controller
    {
        private UserManagementEntities db = new UserManagementEntities();

        // GET: Admin
        public ActionResult AdminHP()
        {
            return View(db.Products.ToList());
        }

        // GET: Admin/Details/5
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

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Description,PriceExpected,OldTime,Status,Photo,PostedBy,Date,RequestedBy,RequestedByDatenTime")] Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    product.Photo = new byte[file.ContentLength];
                    file.InputStream.Read(product.Photo, 0, file.ContentLength);
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,Description,PriceExpected,OldTime,Status,Photo,PostedBy,Date,RequestedBy,RequestedByDatenTime")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Photo = product.Photo;

                
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminHP");
                
            }
            return View(product);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Approve(int id)
        {
            Product product = db.Products.Single(x => x.ProductID == id);
            product.Status = 0;
            db.SaveChanges();
            return RedirectToAction("AdminHP");
        }

        public ActionResult Disapprove(int id)
        {
            Product product = db.Products.Single(x => x.ProductID == id);
            product.Status = 3; ;
            db.SaveChanges();
            return RedirectToAction("AdminHP");
        }

        public ActionResult SetSold(int id)
        {
            Product product = db.Products.Single(x => x.ProductID == id);
            product.Status = 2;
            db.SaveChanges();

            return RedirectToAction("AdminHP"); 
        }
    }

}
