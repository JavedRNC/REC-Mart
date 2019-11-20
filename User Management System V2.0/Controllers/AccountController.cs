using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User_Management_System_V2._0.Models;
using User_Management_System_V2._0.Models.Account;
using WebMatrix.WebData;

namespace User_Management_System_V2._0.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {


            if (WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
             if (ModelState.IsValid)
            {
                bool isAuthenticated = WebSecurity.Login(loginModel.UserName, loginModel.Password, loginModel.RememberMe);
                string returnUrl = Request["ReturnUrl"];
                if (isAuthenticated)
                {
                    if (returnUrl == null)
                    {
                        string name = loginModel.UserName;
                        if (name == "admin33" )
                        {
                            return RedirectToAction("AdminHP", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                    else
                    {
                        string name = loginModel.UserName;
                        if (name == "admin33" )
                        {
                            return RedirectToAction("AdminHP", "Admin");
                        }
                        else
                        return Redirect(Url.Content(returnUrl));
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Username or Password is invalid");
                }
            }

           

            return View();
        }

        public ActionResult SignOut()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login","Account");
        }

        //[Authorize(Users = "admin")]
        public ActionResult Register()
        {
            if(WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
                return View();
            }
            else
            return View();
        }
        [HttpPost]
        
        public ActionResult Register(RegisterModel registerModel)
        {
            if(ModelState.IsValid)
            {
                bool isUserExist = WebSecurity.UserExists(registerModel.UserName);
                
                if (isUserExist)
                {
                    ModelState.AddModelError("", "User Already Exist ! Please Choose different one.");
                }
                else
                {
                    
                       WebSecurity.CreateUserAndAccount(registerModel.UserName, registerModel.Password, new { FullName = registerModel.FullName, Email = registerModel.Email });
                        return RedirectToAction("Index", "Dashboard");
                    
                }


            }
            return View();
        }

        public ActionResult TnC()
        {
            return View();
        }

    }

   
}