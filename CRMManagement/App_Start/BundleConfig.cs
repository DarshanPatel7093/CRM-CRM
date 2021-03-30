using System;
using System.IO.Compression;
using System.Net;
using System.Web;
using System.Web.Optimization;

namespace CRMManagement
{
    #region GZip Compression

    /// <summary>
    /// Class GZipBundle.
    /// </summary>
    public class GZipBundle : Bundle
    {
        public GZipBundle(string virtualPath, params IBundleTransform[] transforms)
            : base(virtualPath, null, transforms) { }

        public override BundleResponse CacheLookup(BundleContext context)
        {
            if (null != context) GZipEncodePage(context.HttpContext);
            return base.CacheLookup(context);
        }

        // Sets up the current page or handler to use GZip through a Response.Filter.
        public static void GZipEncodePage(HttpContextBase httpContext)
        {
            if (null != httpContext && null != httpContext.Request && null != httpContext.Response
                && (null == httpContext.Response.Filter
                || !(httpContext.Response.Filter is GZipStream || httpContext.Response.Filter is DeflateStream)))
            {
                // Is GZip supported?
                string acceptEncoding = httpContext.Request.Headers["Accept-Encoding"];
                if (null != acceptEncoding
                    && acceptEncoding.IndexOf(DecompressionMethods.GZip.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    httpContext.Response.Filter = new GZipStream(httpContext.Response.Filter, CompressionMode.Compress);
                    httpContext.Response.AddHeader("Content-Encoding", DecompressionMethods.GZip.ToString().ToLowerInvariant());
                }
                else if (null != acceptEncoding
                    && acceptEncoding.IndexOf(DecompressionMethods.Deflate.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    httpContext.Response.Filter = new DeflateStream(httpContext.Response.Filter, CompressionMode.Compress);
                    httpContext.Response.AddHeader("Content-Encoding", DecompressionMethods.Deflate.ToString().ToLowerInvariant());
                }

                // Allow proxy servers to cache encoded and unencoded versions separately
                httpContext.Response.AppendHeader("Vary", "Content-Encoding");
            }
        }
    }

    // Represents a bundle that does CSS minification and GZip compression.
    public sealed class GZipStyleBundle : GZipBundle
    {
        public GZipStyleBundle(string virtualPath, params IBundleTransform[] transforms) : base(virtualPath, transforms) { }
    }

    // Represents a bundle that does JS minification and GZip compression.
    public sealed class GZipScriptBundle : GZipBundle
    {
        public GZipScriptBundle(string virtualPath, params IBundleTransform[] transforms)
            : base(virtualPath, transforms)
        {
            base.ConcatenationToken = ";" + Environment.NewLine;
        }
    }

    #endregion 


    public class BundleConfig
    {

        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BEGIN CORE PLUGINS 
            bundles.Add(new GZipScriptBundle("~/bundles/corepulgins", new JsMinify()).Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/js.cookie.min.js",
                        "~/Scripts/jquery.slimscroll.min.js",
                        "~/Scripts/jquery.blockui.min.js",
                        "~/Scripts/bootstrap-switch.min.js"));
            //END CORE PLUGINS

            //BEGIN THEME GLOBAL SCRIPTS
            bundles.Add(new GZipScriptBundle("~/bundles/globalscripts", new JsMinify()).Include(
                        "~/Scripts/app.min.js"));
            //END THEME GLOBAL SCRIPTS

            //BEGIN THEME LAYOUT SCRIPTS
            bundles.Add(new GZipScriptBundle("~/bundles/layoutscripts", new JsMinify()).Include(
                        "~/Scripts/layout.min.js",
                        "~/Scripts/demo.min.js",
                        "~/Scripts/quick-sidebar.min.js",
                        "~/Scripts/quick-nav.min.js"));
            //END THEME LAYOUT SCRIPTS

            //BEGIN GLOBAL MANDATORY STYLES
            bundles.Add(new GZipStyleBundle("~/Styles/globalstyles", new CssMinify()).Include(
                      "~/Styles/font-awesome.min.css",
                      "~/Styles/simple-line-icons.min.css",
                      "~/Styles/bootstrap.min.css",
                      "~/Styles/bootstrap-switch.min.css"));
            //END GLOBAL MANDATORY STYLES

            //BEGIN THEME GLOBAL STYLES
            bundles.Add(new GZipStyleBundle("~/Styles/themestyles", new CssMinify()).Include(
                      "~/Styles/components-md.min.css",
                      "~/Styles/plugins-md.min.css"));
            //END THEME GLOBAL STYLES

            //BEGIN THEME LAYOUT STYLES
            bundles.Add(new GZipStyleBundle("~/Styles/layoutstyles", new CssMinify()).Include(
                      "~/Styles/layout.min.css",
                      "~/Styles/darkblue.min.css",
                      "~/Styles/custom.min.css"));
            //END THEME LAYOUT STYLES

            //BEGIN PAGE LEVEL PLUGINS
            bundles.Add(new GZipScriptBundle("~/bundles/pluginsscripts", new JsMinify()).Include(
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/datatable.js",
                        "~/Scripts/datatables.min.js",
                        "~/Scripts/datatables.bootstrap.js",
                        "~/Scripts/select2.full.min.js",
                          "~/Scripts/bootstrap-multiselect.js"));

            bundles.Add(new GZipStyleBundle("~/Styles/pluginsstyles", new CssMinify()).Include(
                      "~/Styles/toastr.min.css",
                      "~/Styles/datatables.min.css",
                      "~/Styles/datatables.bootstrap.css",
                      "~/Styles/select2.min.css",
                      "~/Styles/select2-bootstrap.min.css",
                      "~/Styles/bootstrap-datetimepicker.min.css",
                      "~/Styles/bootstrap-datepicker3.min.css",
                       "~/Styles/bootstrap-multiselect.css"));
            //END PAGE LEVEL PLUGINS

            //BEGIN PAGE LEVEL SCRIPTS
            bundles.Add(new GZipScriptBundle("~/bundles/pagescripts", new JsMinify()).Include(
                        "~/Scripts/ui-modals.min.js",
                        "~/Scripts/table-datatables-buttons.min.js",
                        "~/Scripts/components-bootstrap-switch.min",
                        //"~/Scripts/components-select2.min.js",
                         "~/Scripts/moment.min.js",
                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/bootstrap-datetimepicker.min.js",
                        "~/Scripts/bootstrap-datepicker.min.js",
                        "~/Scripts/components-bootstrap-multiselect.min.js"));
            //END PAGE LEVEL SCRIPTS

            //JQ VALIDATION
            bundles.Add(new GZipScriptBundle("~/bundles/jqval", new JsMinify()).Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/additional-methods.min.js"));

            //Toaster Popup
            bundles.Add(new GZipScriptBundle("~/bundles/toaster", new JsMinify()).Include(
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/ui-toastr.min.js"));

            //COMMON JS
            bundles.Add(new GZipScriptBundle("~/bundles/commonscripts", new JsMinify()).Include(
                        "~/Scripts/common.js"));
        }
    }
}
