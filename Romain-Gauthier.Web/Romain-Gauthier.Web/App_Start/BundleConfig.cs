using System.Web;
using System.Web.Optimization;

namespace Romain_Gauthier.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            //bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.2.0.js",
                "~/Scripts/knockout.mapping-latest.js"));
            //main
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
               "~/Scripts/js/main.js", "~/Scripts/moment.js", "~/Scripts/moment-with-locales.min.js"));

            //swiper
            bundles.Add(new StyleBundle("~/Content/swiper").Include(
                      "~/Content/swiper.css"));
            //swiper
            bundles.Add(new ScriptBundle("~/bundles/swiper").Include(
               "~/Scripts/swiper.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/animate.css"));
            bundles.Add(new StyleBundle("~/Content/mobilecss").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/MobileSite.css",
                     "~/Content/animate.css"));
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                     "~/Scripts/js/login.js"));
            //animate
            bundles.Add(new StyleBundle("~/Content/animate").Include(
                      "~/Content/animate.css"));

            //brandinfo
            bundles.Add(new StyleBundle("~/Content/brandinfo").Include(
                      "~/Content/brandInfo.css",
                      "~/Content/animate.css"));

            bundles.Add(new ScriptBundle("~/bundles/brandinfo").Include(
                "~/Scripts/js/brandinfo.js"));
            //productinfo
            bundles.Add(new ScriptBundle("~/bundles/productinfo").Include(
              "~/Scripts/js/productinfo.js"));
            //Technology
            bundles.Add(new ScriptBundle("~/bundles/technology").Include(
            "~/Scripts/js/technology.js"));

            //ProductInfo
            bundles.Add(new StyleBundle("~/Content/productInfo").Include(
                      "~/Content/productInfo.css"));
            //ProductInfo
            bundles.Add(new ScriptBundle("~/bundles/productInfo").Include(
            "~/Scripts/js/productInfo.js"));

            //technology
            bundles.Add(new StyleBundle("~/Content/technology").Include(
                      "~/Content/technology.css"));
            //technology
            bundles.Add(new ScriptBundle("~/bundles/technology").Include(
            "~/Scripts/js/technology.js", "~/Scripts/jquery.easing.1.3.js", "~/Scripts/jquery.scrollTo.js"));

            //Polish
            bundles.Add(new StyleBundle("~/Content/polish").Include(
                      "~/Content/polish.css"));
            //Polish
            bundles.Add(new ScriptBundle("~/bundles/polish").Include(
            "~/Scripts/js/polish.js", "~/Scripts/jquery.keyframes.js"));
            //History
            bundles.Add(new StyleBundle("~/Content/history").Include(
                     "~/Content/history.css"));
            //History
            bundles.Add(new ScriptBundle("~/bundles/history").Include(
            "~/Scripts/js/history.js"));
            //News
            bundles.Add(new StyleBundle("~/Content/news").Include(
                     "~/Content/news.css"));
            //News
            bundles.Add(new ScriptBundle("~/bundles/news").Include(
            "~/Scripts/js/news.js"));
            //ADMIN-News
            bundles.Add(new ScriptBundle("~/bundles/admin-news").Include(
            "~/Scripts/js/admin-news.js"));
            //admin-product
            bundles.Add(new ScriptBundle("~/bundles/admin-product").Include(
                "~/Scripts/js/admin-product.js"));
            //admin-personnel
            bundles.Add(new ScriptBundle("~/bundles/admin-product").Include(
                "~/Scripts/js/admin-personnel.js"));
        }
    }
}
