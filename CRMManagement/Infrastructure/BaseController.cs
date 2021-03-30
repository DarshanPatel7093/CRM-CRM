using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRMManagement.Common;
using CRMManagement.Pages;
using System.Web.Mvc;
using CRMManagement.Services.Contract;
using System.Threading;
using System.Globalization;

namespace CRMManagement.Infrastructure
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //if (ProjectSession.UserID == null || ProjectSession.UserID == 0)
                //{
                //    filterContext.Result = new RedirectResult("~/Account/LogIn");
                //    return;
                //}
                HttpCookie myCookie = HttpContext.Request.Cookies["UsersLogin"] as HttpCookie;
                if (myCookie == null)
                {
                    filterContext.Result = new RedirectResult("~/Account/LogIn");
                    return;
                }
                if (!string.IsNullOrEmpty(myCookie.Values["UserID"]))
                {
                    ProjectSession.UserID = Convert.ToInt32(myCookie.Values["UserID"]);
                }if (!string.IsNullOrEmpty(myCookie.Values["UserName"]))
                {
                    ProjectSession.UserName = myCookie.Values["UserName"].ToString();
                }
                if (!string.IsNullOrEmpty(myCookie.Values["AdminRoleID"]))
                {
                    ProjectSession.AdminRoleID = Convert.ToInt32(myCookie.Values["AdminRoleID"]);
                }
                if (!string.IsNullOrEmpty(myCookie.Values["Email"]))
                {
                    ProjectSession.Email = myCookie.Values["Email"].ToString();
                }


                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                //ErrorLogHelper.Log(ex);
                throw ex;
            }
        }
    }
}