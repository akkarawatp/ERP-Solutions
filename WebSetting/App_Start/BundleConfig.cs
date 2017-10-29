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

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/jquery-{version}.min.js",
                        "~/js/jquery.form.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/js/jquery.unobtrusive.min.js",
                        "~/js/jquery.validate.min.js",
                        "~/js/jquery.validate.unobtrusive.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr.min.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/css/Site.css"));

            bundles.Add(new StyleBundle("~/Content/select2").Include(
                        "~/css/select2-bootstrap/select2.css",
                        "~/css/select2-bootstrap/select2-bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/js/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/js/app.js")
                .Include("~/js/bootstraptable/bootstrap-table.min.js")
                .Include("~/js/bootstraptable/bootstrap-table-th-TH.min.js")
                .Include("~/js/autoNumeric.min.js")
                );
            bundles.Add(new StyleBundle("~/Content/bootstrap/base").Include("~/css/bootstrap.min.css", "~/css/base/jquery-ui.css", "~/css/themes/base/jquery.ui.theme.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap/theme").Include("~/css/bootstrap-theme.min.css"));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include("~/css/font-awesome.min.css"));
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
