using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User_Management_System_V2._0.Models;


namespace User_Management_System_V2._0.Controllers
{
    public class DashboardController : Controller
    {
        UserManagementEntities db = new UserManagementEntities();
        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            List<Product> products = db.Products.Where(x =>x.Status==0).ToList();
            return View(products);
        }


        public ActionResult Description(int id)
        {
            Product product = db.Products.Single(x => x.ProductID == id);

            return View(product);
        }

        public ActionResult FeedbacknComplaints()
        {
            return View();
        }
    }
}