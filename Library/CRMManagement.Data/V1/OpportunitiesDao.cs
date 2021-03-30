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
    public class OpportunitiesDao : AbstractOpportunitiesDao
    {
        public override PagedList<AbstractOpportunities> OpportunitiesSelectAll(PageParam pageParam, string Search = "", int CompanyContactId = 0, int AssignedUserId = 0, int StatusId = 0, string StartDate = "", string EndDate = "", int UserId = 0, int DateType = 0)
        {
            PagedList<AbstractOpportunities> Opportunities = new PagedList<AbstractOpportunities>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CompanyContactId", CompanyContactId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StartDate", StartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EndDate", EndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortBy", pageParam.SortBy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortDirection", pageParam.SortDirection, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@DateType", DateType, dbType: DbType.Int32, direction: ParameterDirection.Input);
            if (ProjectSession.AdminRoleID == 3)
            {
                param.Add("@AssignedUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            }
            else
            {
                param.Add("@AssignedUserId", AssignedUserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            }
            param.Add("@StatusId", StatusId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OpportunitiesSelectAll, param, commandType: CommandType.StoredProcedure);
                Opportunities.Values.AddRange(task.Read<Opportunities>());
                Opportunities.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Opportunities;
        }

        public override SuccessResult<AbstractOpportunities> OpportunitiesSelectById(int id)
        {
            SuccessResult<AbstractOpportunities> Opportunities = null;
            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OpportunitiesSelectById, param, commandType: CommandType.StoredProcedure);
                Opportunities = task.Read<SuccessResult<AbstractOpportunities>>().SingleOrDefault();
                Opportunities.Item = task.Read<Opportunities>().SingleOrDefault();
            }
            return Opportunities;
        }

        public override SuccessResult<AbstractOpportunities> OpportunitiesUpsert(AbstractOpportunities abstractOpportunities)
        {
            SuccessResult<AbstractOpportunities> Opportunities = null;
            var param = new DynamicParameters();
            param.Add("@Id", abstractOpportunities.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Name", abstractOpportunities.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Description", abstractOpportunities.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CompanyContactId", abstractOpportunities.CompanyContactId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StatusId", abstractOpportunities.StatusId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@SuccessRatePercent", abstractOpportunities.SuccessRatePercent, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StartDate", abstractOpportunities.StartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EndDate", abstractOpportunities.EndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AssignedUserId", abstractOpportunities.AssignedUserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OpportunitiesUpsert, param, commandType: CommandType.StoredProcedure);
                Opportunities = task.Read<SuccessResult<AbstractOpportunities>>().SingleOrDefault();
                Opportunities.Item = task.Read<Opportunities>().SingleOrDefault();
            }
            return Opportunities;
        }
        public override bool OpportunitiesDelete(int Id)
        {
            bool users = false;
            var param = new DynamicParameters();
            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.OpportunitiesDelete, param, commandType: CommandType.StoredProcedure);
                users = task.SingleOrDefault<bool>();
            }
            return users;
        }

        public override PagedList<AbstractOpportunities> OpportunitiesSelectAllForAsignUser()
        {
            PagedList<AbstractOpportunities> Opportunities = new PagedList<AbstractOpportunities>();
            var param = new DynamicParameters();
            param.Add("@AssignedUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OpportunitiesSelectAllForAsignUser, param, commandType: CommandType.StoredProcedure);
                Opportunities.Values.AddRange(task.Read<Opportunities>());
                Opportunities.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Opportunities;
        }
        public override PagedList<AbstractOpportunities> OpportunitiesSelectAllForAsignUserForFilter()
        {
            PagedList<AbstractOpportunities> Opportunities = new PagedList<AbstractOpportunities>();
            var param = new DynamicParameters();
            param.Add("@AssignedUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OpportunitiesSelectAllForAsignUserForFilter, param, commandType: CommandType.StoredProcedure);
                Opportunities.Values.AddRange(task.Read<Opportunities>());
                Opportunities.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Opportunities;
        }
    }
}
