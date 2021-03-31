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
using System.IO;
using System.Linq;
using System.Web.Mvc;
using static CRMManagement.Infrastructure.Enums;
namespace CRMManagement.Controllers
{
    public class OpportunitiesController : BaseController
    {
        #region Fields

        private readonly AbstractOpportunitiesServices abstractOpportunitiesServices;
        private readonly AbstractUsersServices abstractUsersServices;
        private readonly AbstractStatusDescServices abstractStatusDescServices;
        private readonly AbstractCompanyContactsServices abstractCompanyContactsServices;
        #endregion

        #region Ctor

        public OpportunitiesController(AbstractOpportunitiesServices abstractOpportunitiesServices,
            AbstractUsersServices abstractUsersServices,
            AbstractStatusDescServices abstractStatusDescServices,
            AbstractCompanyContactsServices abstractCompanyContactsServices)
        {
            this.abstractOpportunitiesServices = abstractOpportunitiesServices;
            this.abstractUsersServices = abstractUsersServices;
            this.abstractStatusDescServices = abstractStatusDescServices;
            this.abstractCompanyContactsServices = abstractCompanyContactsServices;

        }

        #endregion

        #region Methods
        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            ViewBag.Status = BindStatusDropdown();
            ViewBag.User = BindUserDropdown();
            ViewBag.CompanyContact = BindCompanyContactDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.BindOpportunities)]
        public JsonResult BindOpportunities([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int CompanyContactId = 0, int AssignedUserId = 0, int StatusId = 0, string StartDate = "", string EndDate = "", int DateType = 0)
        {
            try
            {
                int totalRecord = 0;
                int filteredRecord = 0;
                ProjectSession.OpportunitiesCompanyContactID = CompanyContactId;
                ProjectSession.OpportunitiesAssignedUserID = AssignedUserId;
                ProjectSession.OpportunitiesStatusId = StatusId;
                ProjectSession.OpportunitiesStartDate = StartDate;
                ProjectSession.OpportunitiesEndDate = EndDate;
                ProjectSession.OpportunitiesDateType = DateType;
                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;
                string Search = requestModel.Search.Value;
                pageParam.SortBy = requestModel.Columns.ElementAt(requestModel.OrderColumn).Data;
                pageParam.SortDirection = requestModel.OrderDir.ToUpper() == "DESC" ? "Descending" : "Ascending";

                var model = abstractOpportunitiesServices.OpportunitiesSelectAll(pageParam, Search, CompanyContactId, AssignedUserId, StatusId, StartDate, EndDate, ProjectSession.UserID, DateType);

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
            ViewBag.Status = BindStatusDropdown();
            ViewBag.User = BindUserDropdown();
            ViewBag.CompanyContact = BindCompanyContactDropdown();
            if (Id > 0)
            {
                var model = abstractOpportunitiesServices.OpportunitiesSelectById(Id).Item;
                return View(model);
            }
            return View();
        }

        public IList<SelectListItem> BindUserDropdown()
        {
            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 10000;
            var model = abstractUsersServices.UsersSelectAll(pageParam, "");
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in model.Values)
            {
                items.Add(new SelectListItem() { Text = category.Name.ToString(), Value = category.Id.ToString() });
            }
            return items;
        }

        public IList<SelectListItem> BindCompanyContactDropdown()
        {
            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 10000;
            var model = abstractCompanyContactsServices.CompanyContactsSelectAll(pageParam, "");
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in model.Values)
            {
                items.Add(new SelectListItem() { Text = category.FullName.ToString(), Value = category.Id.ToString() });
            }
            return items;
        }

        public IList<SelectListItem> BindStatusDropdown()
        {
            var model = abstractStatusDescServices.StatusDescAllDropdown();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in model.Values)
            {
                items.Add(new SelectListItem() { Text = category.StatusDesc.ToString(), Value = category.Id.ToString() });
            }
            return items;
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.AddEditOpportunities)]
        public ActionResult AddEditOpportunities(Opportunities Opportunities)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                SuccessResult<AbstractOpportunities> result = null;
                result = abstractOpportunitiesServices.OpportunitiesUpsert(Opportunities);
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
            return RedirectToAction(Actions.Index, Pages.Controllers.Opportunities);
        }

        [HttpPost]
        [ActionName(Actions.OpportunitiesDelete)]
        public JsonResult OpportunitiesDelete(int Id)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                try
                {
                    var abstractSubAdmins = abstractOpportunitiesServices.OpportunitiesDelete(Id);
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

        [HttpGet]
        public ActionResult SendEmail(string id = "MA==")
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            int Id = Convert.ToInt32(ConvertTo.Base64Decode(id));
            if (Id > 0)
            {
                var model = abstractOpportunitiesServices.OpportunitiesSelectById(Id).Item;
                string Email = ProjectSession.Email;
                if (Email != "" && model != null)
                {
                    string Emailtext = "<p style='margin-left: 70px;'>You can see #TASK# Details Below.</p><h5 style='margin-left: 70px;'> Opportunity Name :- <span style='font-weight:bold'>#OPNAME#</span></h5><h5 style='margin-left: 70px;'> Assigned User Name :- <span style='font-weight:bold'>#ASSIGNUSRNAME#</span></h5><h5 style='margin-left: 70px;'> Company Contact Name :- <span style='font-weight:bold'>#COMCONTNAME#</span></h5><h5 style='margin-left: 70px;'> Description :- <span style='font-weight:bold'>#Description#</span></h5><h5 style='margin-left: 70px;'> Status :- <span style='font-weight:bold'>#Status#</span></h5><h5 style='margin-left: 70px;'> Start Date :- <span style='font-weight:bold'>#StartDate#</span></h5><h5 style='margin-left: 70px;'> End Date :- <span style='font-weight:bold'>#EndDate#</span></h5><h5 style='margin-left: 70px;'> Success Rate Percent :- <span style='font-weight:bold'>#SuccessRatePercent#</span></h5>";
                    string body = string.Empty;

                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate/index.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    Emailtext = Emailtext.Replace("#TASK#", "Opportunity");
                    Emailtext = Emailtext.Replace("#OPNAME#", model.Name);
                    Emailtext = Emailtext.Replace("#ASSIGNUSRNAME#", model.AssignedUserName);
                    Emailtext = Emailtext.Replace("#COMCONTNAME#", model.CompanyContactName);
                    Emailtext = Emailtext.Replace("#Description#", model.Description);
                    Emailtext = Emailtext.Replace("#Status#", model.StatusName);
                    Emailtext = Emailtext.Replace("#StartDate#", model.StartDate);
                    Emailtext = Emailtext.Replace("#EndDate#", model.EndDate);
                    Emailtext = Emailtext.Replace("#SuccessRatePercent#", Convert.ToString(model.SuccessRatePercent));
                    body = body.Replace("#TASK#", "Opportunity");
                    body = body.Replace("#USERNAME#", ProjectSession.UserName);
                    body = body.Replace("#MAINMESSAGE#", Emailtext);
                    //EmailHelper.Send(Email, "", "", "CRM Opportunity: " + model.Name, body);

                    System.Net.Mail.Attachment Attachment1 = EmailHelper.AddAttachment("CRM Opportunity: " + model.Name, Emailtext, model.StartDate, model.EndDate);
                    EmailHelper.Send1(Email, "", "", "CRM Opportunity: " + model.Name, body, Attachment1);

                }
            }

            return RedirectToAction(Actions.Index, Pages.Controllers.Opportunities);
        }
        #endregion
    }
}
