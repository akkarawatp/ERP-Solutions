using System;
using System.Web.Optimization;

namespace WebSetting
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.min.js",
            //            "~/Scripts/jquery.form.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive.min.js",
            //            "~/Scripts/jquery.validate.min.js",
            //            "~/Scripts/jquery.validate.unobtrusive.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr.min.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //        "~/Content/Site.css",
            //        "~/Content/bootstrap-responsive.min.css"));

            //bundles.Add(new StyleBundle("~/Content/select2").Include(
            //            "~/Content/select2-bootstrap/select2.css",
            //            "~/Content/select2-bootstrap/select2-bootstrap.css"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.min.js"));
            //bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app.js"));
            //bundles.Add(new StyleBundle("~/Content/bootstrap/base").Include("~/Content/bootstrap.min.css", "~/Content/base/jquery-ui.css", "~/Content/base/jquery.ui.theme.css"));
            //bundles.Add(new StyleBundle("~/Content/bootstrap/theme").Include("~/Content/bootstrap-theme.min.css"));
            //bundles.Add(new StyleBundle("~/Content/font-awesome").Include("~/Content/font-awesome.min.css"));
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }
    }
}
