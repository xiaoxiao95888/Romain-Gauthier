using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Romain_Gauthier.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult News()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult Personnel()
        {
            return View();
        }

        public ActionResult PersonnelGroup()
        {
            return View();
        }
        public ActionResult TrainArticle()
        {
            return View();
        }
        public ActionResult File()
        {
            return View();
        }
        public ActionResult TrainingScoring()
        {
            return View();

        }
        public ActionResult Log()
        {
            return View();

        }
    }
}