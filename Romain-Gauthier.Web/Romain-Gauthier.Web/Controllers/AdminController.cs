﻿using System;
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
    }
}