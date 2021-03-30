using CRMManagement.Common;
using CRMManagement.Common.Paging;
using CRMManagement.Entities.Contract;
using CRMManagement.Entities.V1;
using CRMManagement.Infrastructure;
using CRMManagement.Pages;
using CRMManagement.Services.Contract;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static CRMManagement.Infrastructure.Enums;
namespace CRMManagement.Controllers
{
    public class UsersController : BaseController
    {
        #region Fields

        private readonly AbstractUsersServices abstractUsersServices;
        private readonly AbstractRolesServices abstractRolesServices;
        #endregion

        #region Ctor

        public UsersController(AbstractUsersServices abstractUsersServices,
            AbstractRolesServices abstractRolesServices)
        {
            this.abstractUsersServices = abstractUsersServices;
            this.abstractRolesServices = abstractRolesServices;

        }

        #endregion

        #region Methods
        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            ViewBag.Roles = BindRoleDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.BindUsers)]
        public JsonResult BindUsers([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int RoleId = 0)
        {
            try
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;
                string Search = requestModel.Search.Value;
                pageParam.SortBy = requestModel.Columns.ElementAt(requestModel.OrderColumn).Data;
                pageParam.SortDirection = requestModel.OrderDir.ToUpper() == "DESC" ? "Descending" : "Ascending";

                var model = abstractUsersServices.UsersSelectAll(pageParam, Search, RoleId);

                totalRecord = (int)model.TotalRecords;
                filteredRecord = (int)model.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, model.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new object[] { Enums.MessageType.danger.GetHashCode(), Enums.MessageType.danger.ToString(), "Something Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Manage(string id = "MA==")
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            int Id = Convert.ToInt32(ConvertTo.Base64Decode(id));
            ViewBag.Roles = BindRoleDropdown();
            if (Id > 0)
            {
                var model = abstractUsersServices.UsersSelectById(Id).Item;
                return View(model);
            }
            return View();
        }

        public IList<SelectListItem> BindRoleDropdown()
        {
            var model = abstractRolesServices.RolesAllDropdown();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in model.Values)
            {
                items.Add(new SelectListItem() { Text = category.Roles.ToString(), Value = category.Id.ToString() });
            }
            return items;
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.AddEditUsers)]
        public ActionResult AddEditUsers(Users Users)
        {
            if (ProjectSession.AdminRoleID == 1)
            {
                SuccessResult<AbstractUsers> result = null;
                result = abstractUsersServices.UsersUpsert(Users);
                if (result.Code == 200 && result.Item != null)
                {
                    TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.success.ToString(), result.Message);
                }
                else
                {
                    TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.warning.ToString(), result.Message);
                }
            }
            else
            {
                TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.danger.ToString(), "This user does not have the right to add or edit.");
            }
            return RedirectToAction(Actions.Index, Pages.Controllers.Users);
        }

        [HttpPost]
        [ActionName(Actions.UserDelete)]
        public JsonResult UserDelete(int Id)
        {
            if (ProjectSession.AdminRoleID == 1)
            {
                try
                {
                    var abstractSubAdmins = abstractUsersServices.UserDelete(Id);
                }
                catch (Exception e)
                {
                    TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.danger.ToString(), "Record is not deleted successfully because it refers to another table.");
                }
            }
            else
            {
                TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.danger.ToString(), "This user does not have the right to delete.");
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
