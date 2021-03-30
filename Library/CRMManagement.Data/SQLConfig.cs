//-----------------------------------------------------------------------
// <copyright file="SQLConfig.cs" company="Dexoc Solutions">
//     Copyright Dexoc Solutions. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CRMManagement.Data
{
    /// <summary>
    /// SQL configuration class which holds stored procedure name.
    /// </summary>
    internal sealed class SQLConfig
    {
        #region Users
        public const string UsersSelectAll = "dbo.UsersSelectAll";
        public const string UsersSelectById = "dbo.UsersSelectById";
        public const string UsersUpsert = "dbo.UsersUpsert";
        public const string UserLogin = "dbo.UserLogin";
        public const string UserDelete = "dbo.UserDelete";
        public const string DashboardDetailsByUserId = "dbo.DashboardDetailsByUserId";
        #endregion

        #region Roles
        public const string RolesAllDropdown = "dbo.RolesAllDropdown";
        #endregion

        #region StatusDesc
        public const string StatusDescAllDropdown = "dbo.StatusDescAllDropdown";
        #endregion

        #region Opportunities
        public const string OpportunitiesSelectAll = "dbo.OpportunitiesSelectAll";
        public const string OpportunitiesSelectById = "dbo.OpportunitiesSelectById";
        public const string OpportunitiesUpsert = "dbo.OpportunitiesUpsert";
        public const string OpportunitiesDelete = "dbo.OpportunitiesDelete";
        public const string OpportunitiesSelectAllForAsignUser = "dbo.OpportunitiesSelectAllForAsignUser";
        public const string OpportunitiesSelectAllForAsignUserForFilter = "dbo.OpportunitiesSelectAllForAsignUserForFilter";
        #endregion

        #region Opportunities_Tasks
        public const string Opportunities_TasksSelectAll = "dbo.Opportunities_TasksSelectAll";
        public const string Opportunities_TasksSelectById = "dbo.Opportunities_TasksSelectById";
        public const string Opportunities_TasksUpsert = "dbo.Opportunities_TasksUpsert";
        public const string Opportunities_TasksDelete = "dbo.Opportunities_TasksDelete";
        #endregion
      
        #region CallLog
        public const string CallLogSelectAll = "dbo.CallLogSelectAll";
        public const string CallLogReportSelectAll = "dbo.CallLogReportSelectAll";
        public const string CallLogTypeSelectAll = "dbo.CallLogTypeSelectAll";
        public const string CallLogSelectById = "dbo.CallLogSelectById";
        public const string CallLogUpsert = "dbo.CallLogUpsert";
        public const string CallLogDelete = "dbo.CallLogDelete";
        #endregion

        #region Company
        public const string CompanySelectAll = "dbo.CompanySelectAll";
        public const string CompanySelectById = "dbo.CompanySelectById";
        public const string CompanyUpsert = "dbo.CompanyUpsert";
        public const string CompanyDelete = "dbo.CompanyDelete";
        #endregion

        #region Company
        public const string CompanyContactsSelectAll = "dbo.CompanyContactsSelectAll";
        public const string CompanyContactsSelectById = "dbo.CompanyContactsSelectById";
        public const string CompanyContactsUpsert = "dbo.CompanyContactsUpsert";
        public const string CompanyContactsDelete = "dbo.CompanyContactsDelete";
        #endregion

        #region Projects
        public const string ProjectsSelectAll = "dbo.ProjectsSelectAll";
        public const string ProjectsSelectById = "dbo.ProjectsSelectById";
        public const string ProjectsUpsert = "dbo.ProjectsUpsert";
        public const string ProjectsDelete = "dbo.ProjectsDelete";
        #endregion

        #region ProjectsTimeTracking
        public const string ProjectTimeTrackingSelectAll = "dbo.ProjectTimeTrackingSelectAll";
        public const string ProjectTimeTrackingSelectById = "dbo.ProjectTimeTrackingSelectById";
        public const string ProjectTimeTrackingUpsert = "dbo.ProjectTimeTrackingUpsert";
        public const string ProjectTimeTrackingDelete = "dbo.ProjectTimeTrackingDelete";
        #endregion
    }
}
