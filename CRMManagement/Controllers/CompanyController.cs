using CRMManagement.Common;
using CRMManagement.Common.Paging;
using CRMManagement.Entities.Contract;
using CRMManagement.Entities.V1;
using CRMManagement.Infrastructure;
using CRMManagement.Pages;
using CRMManagement.Services.Contract;
using DataTables.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using static CRMManagement.Infrastructure.Enums;
namespace CRMManagement.Controllers
{
    public class CompanyController : BaseController
    {
        #region Fields

        private readonly AbstractCompanyServices abstractCompanyServices;
        private readonly AbstractRolesServices abstractRolesServices;
        #endregion

        #region Ctor

        public CompanyController(AbstractCompanyServices abstractCompanyServices,
            AbstractRolesServices abstractRolesServices)
        {
            this.abstractCompanyServices = abstractCompanyServices;
            this.abstractRolesServices = abstractRolesServices;

        }

        #endregion

        #region Methods
        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            //ViewBag.Roles = BindRoleDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.BindCompany)]
        public JsonResult BindCompany([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
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

                var model = abstractCompanyServices.CompanySelectAll(pageParam, Search);

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
            //ViewBag.Roles = BindRoleDropdown();
            if (Id > 0)
            {
                var model = abstractCompanyServices.CompanySelectById(Id).Item;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ActionName(Actions.AddEditCompany)]
        public ActionResult AddEditCompany(Company company)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                SuccessResult<AbstractCompany> result = null;
                result = abstractCompanyServices.CompanyUpsert(company);
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

            return RedirectToAction(Actions.Index, Pages.Controllers.Company);
        }

        [HttpPost]
        [ActionName(Actions.CompanyDelete)]
        public JsonResult CompanyDelete(int Id)
        {
            if (ProjectSession.AdminRoleID != 3)
            {
                try
                {
                    var abstractSubAdmins = abstractCompanyServices.CompanyDelete(Id);
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
                var model = abstractCompanyServices.CompanySelectById(Id).Item;
                string Email = ProjectSession.Email;
                if (Email != "" && model != null)
                {
                    string Emailtext = "<p style='margin-left: 70px;'>You can see #TASK# Details Below.</p><h5 style='margin-left: 70px;'> Company Name :- <span style='font-weight:bold'>#COMNAME#</span></h5><h5 style='margin-left: 70px;'> Address1 :- <span style='font-weight:bold'>#Address1#</span></h5><h5 style='margin-left: 70px;'> Address2 :- <span style='font-weight:bold'>#Address2#</span></h5><h5 style='margin-left: 70px;'> Address3 :- <span style='font-weight:bold'>#Address3#</span></h5><h5 style='margin-left: 70px;'> Phone :- <span style='font-weight:bold'>#Phone#</span></h5><h5 style='margin-left: 70px;'> Fax :- <span style='font-weight:bold'>#Fax#</span></h5>";
                    string body = string.Empty;

                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate/index.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    Emailtext = Emailtext.Replace("#TASK#", "Company");
                    Emailtext = Emailtext.Replace("#COMNAME#", model.Name);
                    Emailtext = Emailtext.Replace("#Address1#", model.Address1);
                    Emailtext = Emailtext.Replace("#Address2#", model.Address2);
                    Emailtext = Emailtext.Replace("#Address3#", model.Address3);
                    Emailtext = Emailtext.Replace("#Phone#", model.Phone);
                    Emailtext = Emailtext.Replace("#Fax#", model.Fax);
                    body = body.Replace("#TASK#", "Company");
                    body = body.Replace("#USERNAME#", ProjectSession.UserName);
                    body = body.Replace("#MAINMESSAGE#", Emailtext);
                    EmailHelper.Send(Email, "", "", "Company Details", body);
                }
            }

            return RedirectToAction(Actions.Index, Pages.Controllers.Company);
        }
        #endregion
    }
}
