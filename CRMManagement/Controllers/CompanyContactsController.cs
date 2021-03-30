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
    public class CompanyContactsController : BaseController
    {
        #region Fields

        private readonly AbstractCompanyServices abstractCompanyServices;
        private readonly AbstractCompanyContactsServices abstractCompanyContactsServices;
        #endregion

        #region Ctor

        public CompanyContactsController(AbstractCompanyServices abstractCompanyServices,
            AbstractCompanyContactsServices abstractCompanyContactsServices)
        {
            this.abstractCompanyServices = abstractCompanyServices;
            this.abstractCompanyContactsServices = abstractCompanyContactsServices;

        }

        #endregion

        #region Methods
        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            ViewBag.Company = BindCompanyDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.BindCompanyContacts)]
        public JsonResult BindCompanyContacts([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int CompanyId = 0)
        {
            try
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                ProjectSession.CompanyContactCompanyId = CompanyId;
                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;
                string Search = requestModel.Search.Value;
                pageParam.SortBy = requestModel.Columns.ElementAt(requestModel.OrderColumn).Data;
                pageParam.SortDirection = requestModel.OrderDir.ToUpper() == "DESC" ? "Descending" : "Ascending";

                var model = abstractCompanyContactsServices.CompanyContactsSelectAll(pageParam, Search, CompanyId);

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
            ViewBag.Company = BindCompanyDropdown();
            if (Id > 0)
            {
                var model = abstractCompanyContactsServices.CompanyContactsSelectById(Id).Item;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.AddEditCompanyContacts)]
        public ActionResult AddEditCompanyContacts(CompanyContacts companyContacts)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                SuccessResult<AbstractCompanyContacts> result = null;
                result = abstractCompanyContactsServices.CompanyContactsUpsert(companyContacts);
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

            return RedirectToAction(Actions.Index, Pages.Controllers.CompanyContacts);
        }

        public IList<SelectListItem> BindCompanyDropdown()
        {
            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 0;
            var model = abstractCompanyServices.CompanySelectAll(pageParam, "");
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in model.Values)
            {
                items.Add(new SelectListItem() { Text = category.Name.ToString(), Value = category.Id.ToString() });
            }
            return items;
        }

        [HttpPost]
        [ActionName(Actions.CompanyContactsDelete)]
        public JsonResult CompanyContactsDelete(int Id)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                try
                {
                    var abstractSubAdmins = abstractCompanyContactsServices.CompanyContactsDelete(Id);
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
                var model = abstractCompanyContactsServices.CompanyContactsSelectById(Id).Item;
                string Email = ProjectSession.Email;
                if (Email != "" && model != null)
                {
                    string Emailtext = "<p style='margin-left: 70px;'>You can see #TASK# Details Below.</p><h5 style='margin-left: 70px;'> Company Name :- <span style='font-weight:bold'>#COMNAME#</span></h5><h5 style='margin-left: 70px;'> First Name :- <span style='font-weight:bold'>#FName#</span></h5><h5 style='margin-left: 70px;'> Last Name :- <span style='font-weight:bold'>#LName#</span></h5><h5 style='margin-left: 70px;'> Email :- <span style='font-weight:bold'>#Email#</span></h5><h5 style='margin-left: 70px;'> Phone :- <span style='font-weight:bold'>#Phone#</span></h5>";
                    string body = string.Empty;

                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate/index.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    Emailtext = Emailtext.Replace("#TASK#", "Company Contact");
                    Emailtext = Emailtext.Replace("#COMNAME#", model.CompanyName);
                    Emailtext = Emailtext.Replace("#FName#", model.FName);
                    Emailtext = Emailtext.Replace("#LName#", model.LName);
                    Emailtext = Emailtext.Replace("#Email#", model.Email);
                    Emailtext = Emailtext.Replace("#Phone#", model.Phone);
                    body = body.Replace("#TASK#", "Company Contact");
                    body = body.Replace("#USERNAME#", ProjectSession.UserName);
                    body = body.Replace("#MAINMESSAGE#", Emailtext);
                    EmailHelper.Send(Email, "", "", "Company Contact Details", body);
                }
            }

            return RedirectToAction(Actions.Index, Pages.Controllers.CompanyContacts);
        }
       
        #endregion
    }
}
