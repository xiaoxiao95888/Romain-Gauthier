using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Romain_Gauthier.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Login.";

            return View();
        }
        //BrandInfo
        public ActionResult BrandInfo()
        {
            ViewBag.Message = "BrandInfo.";

            return View();
        }
        public ActionResult ProductInfo()
        {
            return View();
        }
        public ActionResult Technology()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Polish()
        {
            return View();
        }

        public ActionResult History()
        {
            return View();
        }
        public ActionResult News()
        {
            return View();
        }
    }
}