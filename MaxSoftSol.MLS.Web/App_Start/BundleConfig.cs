﻿using System.Web;
using System.Web.Optimization;

namespace MaxSoftSol.MLS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         //"~/Scripts/jquery-{version}.js",
                         //"~/js/jquery.migrate.js",
                         "~/js/bootstrap.min.js",
                          "~/js/jquery.min.js",
                        "~/js/jquery.min.js",
                        "~/js/theme.js"



                        ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      //"~/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(

                      "~/PlugIns/font-awesome/css/font-awesome.min.css",
                      "~/PlugIns/slick/slick.css",
                      "~/PlugIns/slick-nav/slicknav.css",
                      //"~/PlugIns/slick-nav/jquery.slicknav.min",
                      "~/PlugIns//wow/animate.css",
                      "~/assets/css/bootstrap.css",
                      "~/assets/css/theme.css"
                      ));
        }
    }
}
