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
    public class CallLogDao : AbstractCallLogDao
    {
        public override PagedList<AbstractCallLog> CallLogSelectAll(PageParam pageParam, string Search = "", int TaskId = 0, string StartDate = "", string EndDate = "", int DateType = 0)
        {
            PagedList<AbstractCallLog> callLog = new PagedList<AbstractCallLog>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@TaskId", TaskId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@SortBy", pageParam.SortBy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortDirection", pageParam.SortDirection, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@StartDate", StartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EndDate", EndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@DateType", DateType, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CallLogSelectAll, param, commandType: CommandType.StoredProcedure);
                callLog.Values.AddRange(task.Read<CallLog>());
                callLog.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return callLog;
        }
          public override PagedList<AbstractCallLog> CallLogReportSelectAll(PageParam pageParam, string Search = "", string StartDate = "", string EndDate = "")
        {
            PagedList<AbstractCallLog> callLog = new PagedList<AbstractCallLog>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortBy", pageParam.SortBy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortDirection", pageParam.SortDirection, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@StartDate", StartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EndDate", EndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            if(ProjectSession.AdminRoleID == 1)
            {
                param.Add("@AssignedUserId", 0, dbType: DbType.Int32, direction: ParameterDirection.Input);
            }
            else
            {
                param.Add("@AssignedUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            }
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CallLogReportSelectAll, param, commandType: CommandType.StoredProcedure);
                callLog.Values.AddRange(task.Read<CallLog>());
                callLog.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return callLog;
        }

        public override PagedList<AbstractCallLog> CallLogTypeSelectAll()
        {
            PagedList<AbstractCallLog> callLog = new PagedList<AbstractCallLog>();

            var param = new DynamicParameters();
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CallLogTypeSelectAll, param, commandType: CommandType.StoredProcedure);
                callLog.Values.AddRange(task.Read<CallLog>());
                callLog.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return callLog;
        }

        public override SuccessResult<AbstractCallLog> CallLogSelectById(int id)
        {
            SuccessResult<AbstractCallLog> callLog = null;
            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CallLogSelectById, param, commandType: CommandType.StoredProcedure);
                callLog = task.Read<SuccessResult<AbstractCallLog>>().SingleOrDefault();
                callLog.Item = task.Read<CallLog>().SingleOrDefault();
            }
            return callLog;
        }

        public override SuccessResult<AbstractCallLog> CallLogUpsert(AbstractCallLog AbstractCallLog)
        {
            SuccessResult<AbstractCallLog> callLog = null;
            var param = new DynamicParameters();
            param.Add("@Id", AbstractCallLog.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@CallDate", AbstractCallLog.CallDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CallLogType", AbstractCallLog.Type, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ContactName", AbstractCallLog.ContactName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Notes", AbstractCallLog.Notes, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@FollowUpDate", AbstractCallLog.FollowUpDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", AbstractCallLog.CreatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ModifiedBy", AbstractCallLog.ModifiedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@TaskId", AbstractCallLog.TaskId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CallLogUpsert, param, commandType: CommandType.StoredProcedure);
                callLog = task.Read<SuccessResult<AbstractCallLog>>().SingleOrDefault();
                callLog.Item = task.Read<CallLog>().SingleOrDefault();
            }
            return callLog;
        }
        public override bool CallLogDelete(int Id)
        {
            bool users = false;
            var param = new DynamicParameters();
            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.CallLogDelete, param, commandType: CommandType.StoredProcedure);
                users = task.SingleOrDefault<bool>();
            }
            return users;
        }

    }
}
