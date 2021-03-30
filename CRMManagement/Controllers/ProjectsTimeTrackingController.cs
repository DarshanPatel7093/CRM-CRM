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
    public class ProjectsTimeTrackingController : BaseController
    {
        #region Fields

        private readonly AbstractProjectsServices abstractProjectsServices;
        private readonly AbstractProjectsTimeTrackingServices abstractProjectsTimeTrackingServices;
        private readonly AbstractUsersServices abstractUsersServices;
        #endregion

        #region Ctor

        public ProjectsTimeTrackingController(AbstractProjectsServices abstractProjectsServices,
            AbstractProjectsTimeTrackingServices abstractProjectsTimeTrackingServices, AbstractUsersServices abstractUsersServices)
        {
            this.abstractProjectsServices = abstractProjectsServices;
            this.abstractProjectsTimeTrackingServices = abstractProjectsTimeTrackingServices;
            this.abstractUsersServices = abstractUsersServices;

        }

        #endregion

        #region Methods
        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            ViewBag.Project = BindProjectDropdown();
            ViewBag.Users = BindUserDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.BindProjectsTimeTracking)]
        public JsonResult BindProjectsTimeTracking([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int ProjectId = 0, int AssignedUserId = 0, string StartDate = "", string EndDate = "", int DateType = 0)
        {
            try
            {
                int totalRecord = 0;
                int filteredRecord = 0;
                ProjectSession.ProjectsTimeProjectId = ProjectId;
                ProjectSession.ProjectsTimeAssignedUserId = AssignedUserId;
                ProjectSession.ProjectsTimeDateType = DateType;
                ProjectSession.ProjectsTimeStartDate = StartDate;
                ProjectSession.ProjectsTimeEndDate = EndDate;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;
                string Search = requestModel.Search.Value;
                pageParam.SortBy = requestModel.Columns.ElementAt(requestModel.OrderColumn).Data;
                pageParam.SortDirection = requestModel.OrderDir.ToUpper() == "DESC" ? "Descending" : "Ascending";

                var model = abstractProjectsTimeTrackingServices.ProjectTimeTrackingSelectAll(pageParam, Search, ProjectId, AssignedUserId, StartDate, EndDate,DateType);

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
            ViewBag.Project = BindProjectDropdown();
            ViewBag.Users = BindUserDropdown();
            if (Id > 0)
            {
                var model = abstractProjectsTimeTrackingServices.ProjectTimeTrackingSelectById(Id).Item;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.AddEditProjectsTimeTracking)]
        public ActionResult AddEditProjectsTimeTracking(ProjectsTimeTracking projectsTimeTracking)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                SuccessResult<AbstractProjectsTimeTracking> result = null;
                result = abstractProjectsTimeTrackingServices.ProjectTimeTrackingUpsert(projectsTimeTracking);
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
            return RedirectToAction(Actions.Index, Pages.Controllers.ProjectsTimeTracking);
        }

        public IList<SelectListItem> BindProjectDropdown()
        {
            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 0;
            var model = abstractProjectsServices.ProjectsSelectAll(pageParam, "");
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in model.Values)
            {
                items.Add(new SelectListItem() { Text = category.Name.ToString(), Value = category.Id.ToString() });
            }
            return items;
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

        [HttpPost]
        [ActionName(Actions.ProjectTimeTrackingDelete)]
        public JsonResult ProjectTimeTrackingDelete(int Id)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                try
                {
                    var abstractSubAdmins = abstractProjectsTimeTrackingServices.ProjectTimeTrackingDelete(Id);
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
                var model = abstractProjectsTimeTrackingServices.ProjectTimeTrackingSelectById(Id).Item;
                string Email = ProjectSession.Email;
                if (Email != "" && model != null)
                {
                    string Emailtext = "<p style='margin-left: 70px;'>You can see #TASK# Details Below.</p><h5 style='margin-left: 70px;'> Project Name :- <span style='font-weight:bold'>#PRONAME#</span></h5><h5 style='margin-left: 70px;'> UserName :- <span style='font-weight:bold'>#UserName#</span></h5><h5 style='margin-left: 70px;'> Work Description :- <span style='font-weight:bold'>#WorkDescription#</span></h5><h5 style='margin-left: 70px;'> Work Done :- <span style='font-weight:bold'>#WorkDone#</span></h5><h5 style='margin-left: 70px;'> Start Date :- <span style='font-weight:bold'>#StartDate#</span></h5><h5 style='margin-left: 70px;'> End Date :- <span style='font-weight:bold'>#EndDate#</span></h5><h5 style='margin-left: 70px;'> Total Hrs :- <span style='font-weight:bold'>#TotalHrs#</span></h5>";
                    string body = string.Empty;

                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate/index.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    Emailtext = Emailtext.Replace("#TASK#", "Project Time Tracking");
                    Emailtext = Emailtext.Replace("#PRONAME#", model.ProjectName);
                    Emailtext = Emailtext.Replace("#UserName#", model.UserName);
                    Emailtext = Emailtext.Replace("#WorkDescription#", model.WorkDescription);
                    Emailtext = Emailtext.Replace("#WorkDone#", model.WorkDone);
                    Emailtext = Emailtext.Replace("#StartDate#", model.StartDate);
                    Emailtext = Emailtext.Replace("#EndDate#", model.EndDate);
                    Emailtext = Emailtext.Replace("#TotalHrs#", model.TotalHrs.ToString());
                    body = body.Replace("#TASK#", "Project Time Tracking");
                    body = body.Replace("#USERNAME#", ProjectSession.UserName);
                    body = body.Replace("#MAINMESSAGE#", Emailtext);
                    EmailHelper.Send(Email, "", "", "Project Time Tracking Details", body);
                }
            }

            return RedirectToAction(Actions.Index, Pages.Controllers.ProjectsTimeTracking);
        }
        #endregion
    }
}
