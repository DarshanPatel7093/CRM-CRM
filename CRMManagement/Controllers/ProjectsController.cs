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
    public class ProjectsController : BaseController
    {
        #region Fields

        private readonly AbstractProjectsServices abstractProjectsServices;
        private readonly AbstractCompanyContactsServices abstractCompanyContactsServices;
        private readonly AbstractStatusDescServices abstractStatusDescServices;
        #endregion

        #region Ctor

        public ProjectsController(AbstractProjectsServices abstractProjectsServices,
            AbstractCompanyContactsServices abstractCompanyContactsServices, AbstractStatusDescServices abstractStatusDescServices)
        {
            this.abstractProjectsServices = abstractProjectsServices;
            this.abstractCompanyContactsServices = abstractCompanyContactsServices;
            this.abstractStatusDescServices = abstractStatusDescServices;

        }

        #endregion

        #region Methods
        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            ViewBag.Company = BindCompanyContactDropdown();
            ViewBag.Status = BindStatusDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.BindProjects)]
        public JsonResult BindProjects([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int CompanyContactId = 0, int StatusId = 0, string StartDate = "", string EndDate = "", int DateType = 0)
        {
            try
            {
                int totalRecord = 0;
                int filteredRecord = 0;
                ProjectSession.ProjectsCompanyContactID = CompanyContactId;
                ProjectSession.ProjectsStatusId = StatusId;
                ProjectSession.ProjectsDateType = DateType;
                ProjectSession.ProjectsStartDate = StartDate;
                ProjectSession.ProjectsEndDate = EndDate;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;
                string Search = requestModel.Search.Value;
                pageParam.SortBy = requestModel.Columns.ElementAt(requestModel.OrderColumn).Data;
                pageParam.SortDirection = requestModel.OrderDir.ToUpper() == "DESC" ? "Descending" : "Ascending";

                var model = abstractProjectsServices.ProjectsSelectAll(pageParam, Search, CompanyContactId, StatusId,StartDate,EndDate,DateType);

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
            ViewBag.Company = BindCompanyContactDropdown();
            ViewBag.Status = BindStatusDropdown();
            if (Id > 0)
            {
                var model = abstractProjectsServices.ProjectsSelectById(Id).Item;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.AddEditProjects)]
        public ActionResult AddEditProjects(Projects projects)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                SuccessResult<AbstractProjects> result = null;
                result = abstractProjectsServices.ProjectsUpsert(projects);
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
            return RedirectToAction(Actions.Index, Pages.Controllers.Projects);
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
        [ActionName(Actions.ProjectsDelete)]
        public JsonResult ProjectsDelete(int Id)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                try
                {
                    var abstractSubAdmins = abstractProjectsServices.ProjectsDelete(Id);
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
                var model = abstractProjectsServices.ProjectsSelectById(Id).Item;
                string Email = ProjectSession.Email;
                if (Email != "" && model != null)
                {
                    string Emailtext = "<p style='margin-left: 70px;'>You can see #TASK# Details Below.</p><h5 style='margin-left: 70px;'> Project Name :- <span style='font-weight:bold'>#PRONAME#</span></h5><h5 style='margin-left: 70px;'> Description :- <span style='font-weight:bold'>#Description#</span></h5><h5 style='margin-left: 70px;'> Company Contact Name :- <span style='font-weight:bold'>#CompanyContactFullName#</span></h5><h5 style='margin-left: 70px;'> Status :- <span style='font-weight:bold'>#StatusText#</span></h5><h5 style='margin-left: 70px;'> Start Date :- <span style='font-weight:bold'>#StartDate#</span></h5><h5 style='margin-left: 70px;'> End Date :- <span style='font-weight:bold'>#EndDate#</span></h5>";
                    string body = string.Empty;

                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate/index.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    Emailtext = Emailtext.Replace("#TASK#", "Project");
                    Emailtext = Emailtext.Replace("#PRONAME#", model.Name);
                    Emailtext = Emailtext.Replace("#Description#", model.Description);
                    Emailtext = Emailtext.Replace("#CompanyContactFullName#", model.CompanyContactFullName);
                    Emailtext = Emailtext.Replace("#Status#", model.StatusText);
                    Emailtext = Emailtext.Replace("#StartDate#", model.StartDate);
                    Emailtext = Emailtext.Replace("#EndDate#", model.EndDate);
                    body = body.Replace("#TASK#", "Project");
                    body = body.Replace("#USERNAME#", ProjectSession.UserName);
                    body = body.Replace("#MAINMESSAGE#", Emailtext);
                    EmailHelper.Send(Email, "", "", "Project Details", body);
                }
            }

            return RedirectToAction(Actions.Index, Pages.Controllers.Projects);
        }

        #endregion
    }
}
