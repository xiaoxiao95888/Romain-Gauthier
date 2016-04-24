using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Service;
using Romain_Gauthier.Service.Services;
using Romain_Gauthier.Web.Models;
using System.Configuration;

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
            ////测试登录
            //var openId = "oqHRAs6E352cpo0u8OkcMou5e9EQ";
            //FormsAuthentication.SetAuthCookie(openId, false);
            return View();
        }

        public ActionResult TrainArticle()
        {
            ////测试登录
            //var openId = "oqHRAs6E352cpo0u8OkcMou5e9EQ";
            //FormsAuthentication.SetAuthCookie(openId, false);
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
        public ActionResult Accredit()
        {
            var backUrl = "http://romaingauthier.mangoeasy.com/home/login/";
            var state = Guid.NewGuid();
            var weChartloginUrl =
                string.Format(
                    "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect",
                    ConfigurationManager.AppSettings["AppId"], backUrl, state);
            return Redirect(weChartloginUrl);
        }
    }
}