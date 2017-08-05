using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.mvcsso.com.Controllers;

namespace www.domain11.com.controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            username = Request.Form["txtUser"].ToString();
            password = Request.Form["txtPassword"].ToString();
            return base.Login(username, password);
        }
        public ActionResult Loginout()
        {
            return Logout();
            
        }
    }
}