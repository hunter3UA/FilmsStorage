using System.Web;
using System.Web.Optimization;

namespace FilmsStorage
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").
                Include("~/Scripts/jquery-{version}.js")
               .Include( "~/Scripts/jquery.validate*")
               .Include("~/Scripts/modernizr-*")
               .Include("~/Scripts/bootstrap.js")
               .Include("~/Scripts/jquery.unobtrusive-ajax.js"));
                                                                           
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css","~/Content/site.css"));
            // дозволити використання модифікації та оптимізації
            BundleTable.EnableOptimizations = true;       
        }
    }
}
