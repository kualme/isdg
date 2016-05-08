using System.Web;
using System.Web.Optimization;
using Isdg;

namespace Isdg
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var bundle = new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/ckeditor/ckeditor.js",
                        "~/Scripts/ckeditor/adapters/jquery.js",
                        "~/Scripts/Site.js"
                        );
            bundle.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bundle);

            bundles.Add(new ScriptBundle("~/bundles/carousel").Include(
                        "~/Scripts/jcarousellite_*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",                      
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/bootstrap-social.css",
                      "~/Content/sb-admin.css",
                      "~/Content/ckeditor/neo.css",
                      "~/Content/ckeditor/samples.css",
                      "~/Content/site.css"));
        }
    }
}
