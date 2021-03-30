using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataTables.Mvc;
using CRMManagement.Common;
using CRMManagement.Common.Paging;
using CRMManagement.Entities.Contract;
using CRMManagement.Entities.V1;
using CRMManagement.Infrastructure;
using CRMManagement.Pages;
using CRMManagement.Services.Contract;
using static CRMManagement.Infrastructure.Enums;
namespace CRMManagement.Controllers
{
    public class DashboardController : BaseController
    {
        #region Fields

        private readonly AbstractUsersServices UsersServices;
        //private readonly AbstractDashboardServices abstractDashboardServices;
        #endregion

        #region Ctor

        public DashboardController(AbstractUsersServices UsersServices)
        {
            this.UsersServices = UsersServices;
           
        }

        #endregion

        #region Methods
        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            var model = UsersServices.DashboardDetails(ProjectSession.UserID);
            ViewBag.OpportunitiesNumber = model.Item.OpportunitiesNumber;
            ViewBag.OpportunitiesTaskNumber= model.Item.OpportunitiesTaskNumber;
            ViewBag.ProjectsNumber= model.Item.ProjectsNumber;
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName(Actions.BindTopFiveUserFlatsSelectAll)]
        //public JsonResult BindTopFiveUserFlatsSelectAll([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        //{
        //    try
        //    {
        //        int totalRecord = 0;
        //        int filteredRecord = 0;

        //        var model = UsersServices.TopFiveUserFlatsSelectAll();
        //        totalRecord = (int)model.TotalRecords;
        //        filteredRecord = (int)model.TotalRecords;
        //        return Json(new DataTablesResponse(requestModel.Draw, model.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new object[] { null }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName(Actions.BindTopFiveEventsSelectAll)]
        //public JsonResult BindTopFiveEventsSelectAll([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        //{
        //    try
        //    {
        //        int totalRecord = 0;
        //        int filteredRecord = 0;

        //        var model = EventsServices.TopFiveEventsSelectAll();
        //        totalRecord = (int)model.TotalRecords;
        //        filteredRecord = (int)model.TotalRecords;
        //        return Json(new DataTablesResponse(requestModel.Draw, model.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new object[] { null }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName(Actions.BindTopFiveMeetingsSelectAll)]
        //public JsonResult BindTopFiveMeetingsSelectAll([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        //{
        //    try
        //    {
        //        int totalRecord = 0;
        //        int filteredRecord = 0;

        //        var model = EventsServices.TopFiveMeetingsSelectAll();
        //        totalRecord = (int)model.TotalRecords;
        //        filteredRecord = (int)model.TotalRecords;
        //        return Json(new DataTablesResponse(requestModel.Draw, model.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new object[] { null }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName(Actions.BindTopFivePollsSelectAll)]
        //public JsonResult BindTopFivePollsSelectAll([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        //{
        //    try
        //    {
        //        int totalRecord = 0;
        //        int filteredRecord = 0;

        //        var model = PollsServices.TopFivePollsSelectAll();
        //        foreach (var item in model.Values)
        //        {
        //            var polloptions = pollOptionsServices.GetPollOptionsByPollId(item.id).Values;
        //            item.pollOptions = polloptions;
        //        }
        //        totalRecord = (int)model.TotalRecords;
        //        filteredRecord = (int)model.TotalRecords;
        //        return Json(new DataTablesResponse(requestModel.Draw, model.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new object[] { null }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        #endregion
    }
}
