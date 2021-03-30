using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMManagement.Pages
{
    public class Actions
    {
        //Commin Actions

        #region Dashboard
        public const string Index = "Index";
        public const string Manage = "Manage";
        public const string Success = "Success";
        public const string ChangeStatus = "ChangeStatus";
        public const string Delete = "Delete";
        #endregion
        
        #region Account
        public const string LogIn = "LogIn";
        public const string Logout = "Logout";
        #endregion

        #region Users
        public const string BindUsers = "BindUsers";
        public const string AddEditUsers = "AddEditUsers";
        public const string UserDelete = "UserDelete";
        public const string SendEmail = "SendEmail";
        #endregion

        #region Opportunities
        public const string AddEditOpportunities = "AddEditOpportunities";
        public const string BindOpportunities = "BindOpportunities";
        public const string OpportunitiesDelete = "OpportunitiesDelete";
        #endregion

        #region Opportunities_Tasks
        public const string BindCallLog = "BindCallLog";
        public const string BindCallLogReport = "BindCallLogReport";
        public const string BindOpportunities_Tasks = "BindOpportunities_Tasks";
        public const string AddEditOpportunities_Tasks = "AddEditOpportunities_Tasks";
        public const string AddEditTasks_CallLog = "AddEditTasks_CallLog";
        public const string Opportunities_TasksDelete = "Opportunities_TasksDelete";
        public const string CallLogDelete = "CallLogDelete";
        public const string GetCallLogById = "GetCallLogById";
        public const string CallLogReport = "CallLogReport";
        #endregion

        #region Company
        public const string BindCompany = "BindCompany";
        public const string AddEditCompany = "AddEditCompany";
        public const string CompanyDelete = "CompanyDelete";
        #endregion

        #region CompanyContacts
        public const string BindCompanyContacts = "BindCompanyContacts";
        public const string AddEditCompanyContacts = "AddEditCompanyContacts";
        public const string CompanyContactsDelete = "CompanyContactsDelete";
        #endregion

        #region Projects
        public const string BindProjects = "BindProjects";
        public const string AddEditProjects = "AddEditProjects";
        public const string ProjectsDelete = "ProjectsDelete";
        #endregion
        
        #region ProjectsTimeTracking
        public const string BindProjectsTimeTracking = "BindProjectsTimeTracking";
        public const string AddEditProjectsTimeTracking = "AddEditProjectsTimeTracking";
        public const string ProjectTimeTrackingDelete = "ProjectTimeTrackingDelete";
        #endregion

    }
}