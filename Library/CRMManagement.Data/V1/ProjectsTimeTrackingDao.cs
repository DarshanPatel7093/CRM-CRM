using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMManagement.Common;
using CRMManagement.Common.Paging;
using CRMManagement.Data.Contract;
using CRMManagement.Entities.Contract;
using CRMManagement.Entities.V1;

namespace CRMManagement.Data.V1
{
    public class ProjectsTimeTrackingDao : AbstractProjectsTimeTrackingDao
    {
        public override PagedList<AbstractProjectsTimeTracking> ProjectTimeTrackingSelectAll(PageParam pageParam, string Search = "",int ProjectId = 0, int AssignedUserId = 0, string StartDate = "", string EndDate = "", int DateType = 0)
        {
            PagedList<AbstractProjectsTimeTracking> users = new PagedList<AbstractProjectsTimeTracking>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProjectId", ProjectId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StartDate", StartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EndDate", EndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortBy", pageParam.SortBy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortDirection", pageParam.SortDirection, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@DateType", DateType, dbType: DbType.Int32, direction: ParameterDirection.Input);

            if (ProjectSession.AdminRoleID == 3)
            {
                param.Add("@AssignedUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            }
            else
            {
                param.Add("@AssignedUserId", AssignedUserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            }
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProjectTimeTrackingSelectAll, param, commandType: CommandType.StoredProcedure);
                users.Values.AddRange(task.Read<ProjectsTimeTracking>());
                users.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return users;
        }

        public override SuccessResult<AbstractProjectsTimeTracking> ProjectTimeTrackingSelectById(int id)
        {
            SuccessResult<AbstractProjectsTimeTracking> company = null;
            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProjectTimeTrackingSelectById, param, commandType: CommandType.StoredProcedure);
                company = task.Read<SuccessResult<AbstractProjectsTimeTracking>>().SingleOrDefault();
                company.Item = task.Read<ProjectsTimeTracking>().SingleOrDefault();
            }
            return company;
        }

        public override SuccessResult<AbstractProjectsTimeTracking> ProjectTimeTrackingUpsert(AbstractProjectsTimeTracking abstractProjectsTimeTracking)
        {
            SuccessResult<AbstractProjectsTimeTracking> company = null;
            var param = new DynamicParameters();
            param.Add("@Id", abstractProjectsTimeTracking.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@WorkDone", abstractProjectsTimeTracking.WorkDone, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@WorkDescription", abstractProjectsTimeTracking.WorkDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProjectId", abstractProjectsTimeTracking.ProjectId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@AssignedUserId", abstractProjectsTimeTracking.AssignedUserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StartDate", abstractProjectsTimeTracking.StartDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add("@EndDate", abstractProjectsTimeTracking.EndDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add("@TotalHrs", abstractProjectsTimeTracking.TotalHrs, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProjectTimeTrackingUpsert, param, commandType: CommandType.StoredProcedure);
                company = task.Read<SuccessResult<AbstractProjectsTimeTracking>>().SingleOrDefault();
                company.Item = task.Read<ProjectsTimeTracking>().SingleOrDefault();
            }
            return company;
        }
        public override bool ProjectTimeTrackingDelete(int Id)
        {
            bool users = false;
            var param = new DynamicParameters();
            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.ProjectTimeTrackingDelete, param, commandType: CommandType.StoredProcedure);
                users = task.SingleOrDefault<bool>();
            }
            return users;
        }

    }
}
