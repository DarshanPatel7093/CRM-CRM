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
    public class UsersDao : AbstractUsersDao
    {
        public override SuccessResult<AbstractUsers> UserLogin(AbstractUsers abstractUsers, int Type)
        {
            SuccessResult<AbstractUsers> users = null;

            var param = new DynamicParameters();
            param.Add("@Id", abstractUsers.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@UserName", abstractUsers.EmailAddress, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Password", abstractUsers.Password, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserLogin, param, commandType: CommandType.StoredProcedure);
                users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                users.Item = task.Read<Users>().SingleOrDefault();
            }
            return users;
        }

        public override PagedList<AbstractUsers> UsersSelectAll(PageParam pageParam, string Search = "", int RoleId = 0)
        {
            PagedList<AbstractUsers> users = new PagedList<AbstractUsers>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@RoleId", RoleId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@SortBy", pageParam.SortBy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortDirection", pageParam.SortDirection, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UsersSelectAll, param, commandType: CommandType.StoredProcedure);
                users.Values.AddRange(task.Read<Users>());
                users.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return users;
        }

        public override SuccessResult<AbstractUsers> UsersSelectById(int id)
        {
            SuccessResult<AbstractUsers> users = null;
            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UsersSelectById, param, commandType: CommandType.StoredProcedure);
                users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                users.Item = task.Read<Users>().SingleOrDefault();
            }
            return users;
        }

        public override SuccessResult<AbstractUsers> UsersUpsert(AbstractUsers abstractUsers)
        {
            SuccessResult<AbstractUsers> users = null;
            var param = new DynamicParameters();
            param.Add("@Id", abstractUsers.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Name", abstractUsers.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Password", abstractUsers.Password, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Login", abstractUsers.Login, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EmailAddress", abstractUsers.EmailAddress, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@RoleId", abstractUsers.RoleId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UsersUpsert, param, commandType: CommandType.StoredProcedure);
                users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                users.Item = task.Read<Users>().SingleOrDefault();
            }
            return users;
        }

        public override bool UserDelete(int Id)
        {
            bool users = false;
            var param = new DynamicParameters();
            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.UserDelete, param, commandType: CommandType.StoredProcedure);
                users = task.SingleOrDefault<bool>();
            }
            return users;
        }

        public override SuccessResult<AbstractUsers> DashboardDetails(int UserId)
        {
            SuccessResult<AbstractUsers> users = null;
            var param = new DynamicParameters();
            param.Add("@UserId", UserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.DashboardDetailsByUserId, param, commandType: CommandType.StoredProcedure);
                users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                users.Item = task.Read<Users>().SingleOrDefault();
            }
            return users;
        }
    }
}
