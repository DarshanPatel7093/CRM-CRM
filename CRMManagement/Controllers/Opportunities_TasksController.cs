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
    public class Opportunities_TasksController : BaseController
    {
        #region Fields

        private readonly AbstractOpportunities_TasksServices abstractOpportunities_TasksServices;
        private readonly AbstractUsersServices abstractUsersServices;
        private readonly AbstractOpportunitiesServices abstractOpportunitiesServices;
        private readonly AbstractStatusDescServices abstractStatusDescServices;
        private readonly AbstractCallLogServices abstractCallLogServices;
        #endregion

        #region Ctor

        public Opportunities_TasksController(AbstractOpportunities_TasksServices abstractOpportunities_TasksServices,
            AbstractUsersServices abstractUsersServices
            , AbstractOpportunitiesServices abstractOpportunitiesServices
            , AbstractStatusDescServices abstractStatusDescServices, AbstractCallLogServices abstractCallLogServices)
        {
            this.abstractOpportunities_TasksServices = abstractOpportunities_TasksServices;
            this.abstractUsersServices = abstractUsersServices;
            this.abstractOpportunitiesServices = abstractOpportunitiesServices;
            this.abstractStatusDescServices = abstractStatusDescServices;
            this.abstractCallLogServices = abstractCallLogServices;

        }

        #endregion

        #region Methods
        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            ViewBag.User = BindUserDropdown();
            ViewBag.Opportunities = BindOpportunitiesDropdown2();
            ViewBag.Status = BindStatusDropdown();
            ViewBag.CallLogType = BindCallLogDropdown();
            return View();
        }

        public ActionResult CallLogReport()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.BindOpportunities_Tasks)]
        public JsonResult BindOpportunities_Tasks([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int OpportunityId = 0, int AssignedUserId = 0, int StatusId = 0, string StartDate = "", string EndDate = "")
        {
            try
            {
                int totalRecord = 0;
                int filteredRecord = 0;
                ProjectSession.OpportunitiesTaskOppotunitiesID = OpportunityId;
                ProjectSession.OpportunitiesTaskUserID = AssignedUserId;
                ProjectSession.OpportunitiesTaskStatusID = StatusId;
                ProjectSession.OpportunitiesTaskStartDate = StartDate;
                ProjectSession.OpportunitiesTaskEndDate = EndDate;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;
                string Search = requestModel.Search.Value;

                if (requestModel.Draw == 1)
                {
                    pageParam.SortBy = requestModel.Columns.ElementAt(5).Data;
                    pageParam.SortDirection = "Descending";
                }
                else
                {
                    pageParam.SortBy = requestModel.Columns.ElementAt(requestModel.OrderColumn).Data;
                    pageParam.SortDirection = requestModel.OrderDir.ToUpper() == "DESC" ? "Descending" : "Ascending";
                }

                var model = abstractOpportunities_TasksServices.Opportunities_TasksSelectAll(pageParam, Search, OpportunityId, AssignedUserId, StatusId, StartDate, EndDate);

                totalRecord = (int)model.TotalRecords;
                filteredRecord = (int)model.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, model.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new object[] { Enums.MessageType.danger.GetHashCode(), Enums.MessageType.danger.ToString(), "Something Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.BindCallLog)]
        public JsonResult BindCallLog([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string TaskId = "MA==", string StartDate = "", string EndDate = "", int DateType = 0)
        {
            try
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                ProjectSession.CallLogDateType = DateType;
                ProjectSession.CallLogStartDate = StartDate;
                ProjectSession.CallLogEndDate = EndDate;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;
                string Search = requestModel.Search.Value;

                if (requestModel.Draw == 1)
                {
                    pageParam.SortBy = requestModel.Columns.ElementAt(5).Data;
                    pageParam.SortDirection = "Descending";
                }
                else
                {
                    pageParam.SortBy = requestModel.Columns.ElementAt(requestModel.OrderColumn).Data;
                    pageParam.SortDirection = requestModel.OrderDir.ToUpper() == "DESC" ? "Descending" : "Ascending";
                }

                var model = abstractCallLogServices.CallLogSelectAll(pageParam, Search, Convert.ToInt32(ConvertTo.Base64Decode(TaskId)), StartDate, EndDate, DateType);

                totalRecord = (int)model.TotalRecords;
                filteredRecord = (int)model.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, model.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new object[] { Enums.MessageType.danger.GetHashCode(), Enums.MessageType.danger.ToString(), "Something Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.BindCallLogReport)]
        public JsonResult BindCallLogReport([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string StartDate = "", string EndDate = "")
        {
            try
            {
                int totalRecord = 0;
                int filteredRecord = 0;
                ProjectSession.CallLogReportStartDate = StartDate;
                ProjectSession.CallLogReportEndDate = EndDate;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;
                string Search = requestModel.Search.Value;

                pageParam.SortBy = requestModel.Columns.ElementAt(requestModel.OrderColumn).Data;
                pageParam.SortDirection = requestModel.OrderDir.ToUpper() == "DESC" ? "Descending" : "Ascending";

                var model = abstractCallLogServices.CallLogReportSelectAll(pageParam, Search, StartDate, EndDate);

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
            ViewBag.User = BindUserDropdown();
            ViewBag.Opportunities = BindOpportunitiesDropdown();
            ViewBag.Status = BindStatusDropdown();
            if (Id > 0)
            {
                var model = abstractOpportunities_TasksServices.Opportunities_TasksSelectById(Id).Item;
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

        public IList<SelectListItem> BindCallLogDropdown()
        {
            var model = abstractCallLogServices.CallLogTypeSelectAll();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in model.Values)
            {
                items.Add(new SelectListItem() { Text = category.Type.ToString(), Value = category.Id.ToString() });
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

        public IList<SelectListItem> BindOpportunitiesDropdown()
        {
            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 10000;
            PagedList<AbstractOpportunities> model = new PagedList<AbstractOpportunities>();
            if (ProjectSession.AdminRoleID != 3)
            {
                model = abstractOpportunitiesServices.OpportunitiesSelectAll(pageParam, "");
            }
            else
            {
                model = abstractOpportunitiesServices.OpportunitiesSelectAllForAsignUser();
            }

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in model.Values)
            {
                items.Add(new SelectListItem() { Text = category.Name.ToString(), Value = category.Id.ToString() });
            }
            return items;
        }

        public IList<SelectListItem> BindOpportunitiesDropdown2()
        {
            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 10000;
            PagedList<AbstractOpportunities> model = new PagedList<AbstractOpportunities>();
            if (ProjectSession.AdminRoleID != 3)
            {
                model = abstractOpportunitiesServices.OpportunitiesSelectAll(pageParam, "");
            }
            else
            {
                model = abstractOpportunitiesServices.OpportunitiesSelectAllForAsignUserForFilter();
            }

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in model.Values)
            {
                items.Add(new SelectListItem() { Text = category.Name.ToString(), Value = category.Id.ToString() });
            }
            return items;
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.AddEditOpportunities_Tasks)]
        public ActionResult AddEditOpportunities_Tasks(Opportunities_Tasks Opportunities_Tasks)
        {

            SuccessResult<AbstractOpportunities_Tasks> result = null;
            result = abstractOpportunities_TasksServices.Opportunities_TasksUpsert(Opportunities_Tasks);
            if (result.Code == 200 && result.Item != null)
            {
                TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.success.ToString(), result.Message);
            }
            else
            {
                TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.warning.ToString(), result.Message);
            }

            return RedirectToAction(Actions.Index, Pages.Controllers.Opportunities_Tasks);
        }

        [HttpGet]
        public JsonResult GetCallLogById(string id = "MA==")
        {
            int Id = Convert.ToInt32(ConvertTo.Base64Decode(id));
            AbstractCallLog model = new CallLog();
            if (Id > 0)
            {
                model = abstractCallLogServices.CallLogSelectById(Id).Item;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName(Actions.AddEditTasks_CallLog)]
        public JsonResult AddEditTasks_CallLog(CallLog callLog)
        {

            SuccessResult<AbstractCallLog> result = null;
            result = abstractCallLogServices.CallLogUpsert(callLog);
            if (result.Code == 200 && result.Item != null)
            {
                TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.success.ToString(), result.Message);
            }
            else
            {
                TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.warning.ToString(), result.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName(Actions.Opportunities_TasksDelete)]
        public JsonResult Opportunities_TasksDelete(int Id)
        {

            try
            {
                var abstractSubAdmins = abstractOpportunities_TasksServices.Opportunities_TasksDelete(Id);
            }
            catch (Exception e)
            {
                TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.danger.ToString(), "Record is not deleted successfully because it refers to another table.");
            }

            return Json(1, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName(Actions.CallLogDelete)]
        public JsonResult CallLogDelete(int Id)
        {

            try
            {
                var abstractSubAdmins = abstractCallLogServices.CallLogDelete(Id);
            }
            catch (Exception e)
            {
                TempData["openPopup"] = CommonHelper.ShowAlertMessageToastr(MessageType.danger.ToString(), "Record is not deleted successfully because it refers to another table.");
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
                var model = abstractOpportunities_TasksServices.Opportunities_TasksSelectById(Id).Item;
                string Email = ProjectSession.Email;
                if (Email != "" && model != null)
                {
                    string Emailtext = "<p style='margin-left: 70px;'>You can see #TASK# Details Below.</p><h5 style='margin-left: 70px;'> Opportunity Name :- <span style='font-weight:bold'>#OPNAME#</span></h5><h5 style='margin-left: 70px;'> Assigned User Name :- <span style='font-weight:bold'>#ASSIGNUSRNAME#</span></h5><h5 style='margin-left: 70px;'> Opportunity Task Name :- <span style='font-weight:bold'>#OPTASKNAME#</span></h5><h5 style='margin-left: 70px;'> Description :- <span style='font-weight:bold'>#Description#</span></h5><h5 style='margin-left: 70px;'> Follow Up Date :- <span style='font-weight:bold'>#FollowedUpDate#</span></h5><h5 style='margin-left: 70px;'> Notes :- <span style='font-weight:bold'>#Notes#</span></h5>";
                    string body = string.Empty;

                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate/index.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    Emailtext=Emailtext.Replace("#TASK#", "Opportunity Task");
                    Emailtext=Emailtext.Replace("#OPNAME#", model.OpportunityName);
                    Emailtext=Emailtext.Replace("#ASSIGNUSRNAME#", model.AssignedUserName);
                    Emailtext=Emailtext.Replace("#OPTASKNAME#", model.Name);
                    Emailtext=Emailtext.Replace("#Description#", model.Description);
                    Emailtext=Emailtext.Replace("#FollowedUpDate#", model.FollowupDate);
                    Emailtext=Emailtext.Replace("#Notes#", model.Notes);
                    body = body.Replace("#TASK#", "Opportunity Task");
                    body = body.Replace("#USERNAME#", ProjectSession.UserName);
                    body = body.Replace("#MAINMESSAGE#", Emailtext);
                    System.Net.Mail.Attachment Attachment1 = EmailHelper.AddAttachment("CRM Task: " + model.Name, Emailtext, model.FollowupDate, model.FollowupDate);
                    EmailHelper.Send1(Email, "", "","CRM Task: " + model.Name, body,  Attachment1);
                }
            }

            return RedirectToAction(Actions.Index, Pages.Controllers.Opportunities_Tasks);
        }

        #endregion
    }
}
