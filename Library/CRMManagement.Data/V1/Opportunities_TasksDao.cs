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
    public class Opportunities_TasksDao : AbstractOpportunities_TasksDao
    {
        public override PagedList<AbstractOpportunities_Tasks> Opportunities_TasksSelectAll(PageParam pageParam, string Search = "", int OpportunityId = 0, int AssignedUserId = 0, int StatusId = 0, string StartDate = "", string EndDate = "")
        {
            PagedList<AbstractOpportunities_Tasks> Opportunities_Tasks = new PagedList<AbstractOpportunities_Tasks>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@OpportunityId", OpportunityId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StartDate", StartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EndDate", EndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortBy", pageParam.SortBy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortDirection", pageParam.SortDirection, dbType: DbType.String, direction: ParameterDirection.Input);
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
                var task = con.QueryMultiple(SQLConfig.Opportunities_TasksSelectAll, param, commandType: CommandType.StoredProcedure);
                Opportunities_Tasks.Values.AddRange(task.Read<Opportunities_Tasks>());
                Opportunities_Tasks.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Opportunities_Tasks;
        }

        public override SuccessResult<AbstractOpportunities_Tasks> Opportunities_TasksSelectById(int id)
        {
            SuccessResult<AbstractOpportunities_Tasks> Opportunities_Tasks = null;
            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Opportunities_TasksSelectById, param, commandType: CommandType.StoredProcedure);
                Opportunities_Tasks = task.Read<SuccessResult<AbstractOpportunities_Tasks>>().SingleOrDefault();
                Opportunities_Tasks.Item = task.Read<Opportunities_Tasks>().SingleOrDefault();
            }
            return Opportunities_Tasks;
        }

        public override SuccessResult<AbstractOpportunities_Tasks> Opportunities_TasksUpsert(AbstractOpportunities_Tasks abstractOpportunities_Tasks)
        {
            SuccessResult<AbstractOpportunities_Tasks> Opportunities_Tasks = null;
            var param = new DynamicParameters();
            param.Add("@Id", abstractOpportunities_Tasks.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@OpportunityId", abstractOpportunities_Tasks.OpportunityId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@AssignedUserId", abstractOpportunities_Tasks.AssignedUserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Name", abstractOpportunities_Tasks.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Description", abstractOpportunities_Tasks.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@StatusId", abstractOpportunities_Tasks.StatusId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@FollowupDate", abstractOpportunities_Tasks.FollowupDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Notes", abstractOpportunities_Tasks.Notes, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Opportunities_TasksUpsert, param, commandType: CommandType.StoredProcedure);
                Opportunities_Tasks = task.Read<SuccessResult<AbstractOpportunities_Tasks>>().SingleOrDefault();
                Opportunities_Tasks.Item = task.Read<Opportunities_Tasks>().SingleOrDefault();
            }
            return Opportunities_Tasks;
        }
        public override bool Opportunities_TasksDelete(int Id)
        {
            bool users = false;
            var param = new DynamicParameters();
            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.Opportunities_TasksDelete, param, commandType: CommandType.StoredProcedure);
                users = task.SingleOrDefault<bool>();
            }
            return users;
        }

    }
}
