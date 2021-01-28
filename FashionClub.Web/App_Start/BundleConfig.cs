using System.Web;
using System.Web.Optimization;

namespace FashionClub.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Content/js/vendor/jquery-1.12.0.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryOthers").Include(
                      "~/Content/js/jquery.magnific-popup.min.js",
                      "~/Content/js/jquery.counterup.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/cookie").Include(
                      "~/Scripts/jquery.cookie-1.4.1.min.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                      "~/Content/js/popper.js"
                      ));
             bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/bootstrap.min.js"
                      ));
            
            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
                      "~/Areas/Dashboard/Content/sweetalert/sweetalert.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/carousel").Include(
                      "~/Content/js/owl.carousel.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      
                      "~/Content/js/isotope.pkgd.min.js",
                      "~/Content/js/imagesloaded.pkgd.min.js",
                      "~/Content/js/waypoints.min.js",
                      "~/Content/js/ajax-mail.js",
                      "~/Content/js/plugins.js",
                      "~/Content/js/main.js"
                      ));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/magnific-popup.css",
                      "~/Content/css/owl.carousel.min.css",
                      "~/Content/css/meanmenu.min.css",
                      "~/Content/css/bundle.css",
                      "~/Content/css/style.css",
                      "~/Content/css/responsive.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/icons").Include(
                      "~/Content/css/themify-icons.css",
                      "~/Content/css/pe-icon-7-stroke.css",
                      "~/Content/css/animate.css"
                      ));
        }
    }
}
