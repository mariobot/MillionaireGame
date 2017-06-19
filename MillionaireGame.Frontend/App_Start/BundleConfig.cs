using System.Web.Optimization;

namespace MillionaireGame.Frontend.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //custom player name validation script
            bundles.Add(new ScriptBundle("~/bundles/nameval").Include(
                    "~/Scripts/Custom/name-validator.js"));

            bundles.Add(new ScriptBundle("~/bundles/redirect").Include(
                    "~/Scripts/jquery.redirect.js"));

            bundles.Add(new ScriptBundle("~/bundles/answer").Include(
                    "~/Scripts/Custom/user-answer.js",
                    "~/Scripts/Custom/fifty-percents-hint.js",
                    "~/Scripts/Custom/friend-call-hint.js",
                    "~/Scripts/Custom/audience-hint.js"));
        }
    }
}