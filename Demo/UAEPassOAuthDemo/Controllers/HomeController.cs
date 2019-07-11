using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAEPassOAuthCodeLibrary;

namespace UAEPassOAuthDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "UAE Pass oAuth Demo.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Feel free to write me on sikandar.amla@gmail.com";

            return View();
        }

        public ActionResult UserInfo(UAEPassUser userDetails)
        {
            return View(userDetails);
        }
    }
}