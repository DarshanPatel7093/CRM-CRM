using CRMManagement.Common;
using CRMManagement.Entities.Contract;
using CRMManagement.Entities.V1;
using CRMManagement.Pages;
using CRMManagement.Services.Contract;
using System;
using System.Web;
using System.Web.Mvc;
using static CRMManagement.Infrastructure.Enums;

namespace CRMManagement.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        private readonly AbstractUsersServices usersService;
        #endregion

        #region Ctor
        public AccountController(AbstractUsersServices usersService)
        {
            this.usersService = usersService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [ActionName(Actions.LogIn)]
        public ActionResult LogIn()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.LogIn)]
        public ActionResult LogIn(Users objmodel)
        {
            SuccessResult<AbstractUsers> result = usersService.UserLogin(objmodel, 1);
            if (result != null && result.Code == 200 && result.Item != null)
            {
                objmodel.Id = result.Item.Id;
                result = usersService.UserLogin(objmodel, 2);
                if (result != null && result.Code == 200 && result.Item != null)
                {
                    Session.Clear();
                    ProjectSession.UserID = result.Item.Id;
                    ProjectSession.UserName = result.Item.Name;
                    ProjectSession.AdminRoleID = result.Item.RoleId;
                    ProjectSession.Email = result.Item.EmailAddress;

                    HttpCookie cookie = new HttpCookie("UsersLogin");
                    cookie.Values.Add("UserID", objmodel.Id.ToString());
                    cookie.Values.Add("UserName", result.Item.Name.ToString());
                    cookie.Values.Add("AdminRoleID", result.Item.RoleId.ToString());
                    cookie.Values.Add("Email", result.Item.EmailAddress.ToString());
                    cookie.Expires = DateTime.Now.AddHours(24);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction(Actions.Index, "Dashboard");

                }
                else
                {
                    result.Message = "Oops!..You are inactive right now!";
                    ViewBag.openPopup = CommonHelper.ShowAlertMessageToastr(MessageType.danger.ToString(), result.Message);
                }
            }
            else
            {
                ViewBag.openPopup = CommonHelper.ShowAlertMessageToastr(MessageType.danger.ToString(), result.Message);
            }
            return View();
        }

        [AllowAnonymous]
        [ActionName(Actions.Logout)]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            return RedirectToAction(Actions.LogIn, Pages.Controllers.Account, new { Area = "" });
        }
        
        #endregion
    }
}