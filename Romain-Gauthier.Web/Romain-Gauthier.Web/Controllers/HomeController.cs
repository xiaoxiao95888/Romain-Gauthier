﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Service;
using Romain_Gauthier.Service.Services;
using Romain_Gauthier.Web.Models;

namespace Romain_Gauthier.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService _nwesService;
        public HomeController()
        {
            _nwesService = new NewsService(new RomainGauthierDataContext());
        }
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

        public ActionResult ImageDownload()
        {
            return View();
        }

        public ActionResult TrainArticle()
        {
            //测试登录
            var userId = "D2C3C9C7-81CD-4AE5-81AB-53C702072FBA";
            FormsAuthentication.SetAuthCookie(userId, false);
            return View();
        }
        public ActionResult TrainArticleDetail()
        {
            return View();
        }

        public ActionResult NewsDetail(Guid id)
        {
            var item = _nwesService.GetNews(id);
            var model = new NewsModel
            {
                Id = item.Id,
                Content = item.Content,
                Title = item.Title,
                UpdateTime = item.UpdateTime,
                NewsTypeName = item.NewsType.Name
            };
            return View(model);
        }
    }
}