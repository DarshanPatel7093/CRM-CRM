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
    public class ProjectsDao : AbstractProjectsDao
    {
        public override PagedList<AbstractProjects> ProjectsSelectAll(PageParam pageParam, string Search = "",int CompanyContactId=0,int StatusId=0, string StartDate = "", string EndDate = "", int DateType = 0)
        {
            PagedList<AbstractProjects> users = new PagedList<AbstractProjects>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CompanyContactId", CompanyContactId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StatusId", StatusId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StartDate", StartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EndDate", EndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortBy", pageParam.SortBy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortDirection", pageParam.SortDirection, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@DateType", DateType, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProjectsSelectAll, param, commandType: CommandType.StoredProcedure);
                users.Values.AddRange(task.Read<Projects>());
                users.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return users;
        }

        public override SuccessResult<AbstractProjects> ProjectsSelectById(int id)
        {
            SuccessResult<AbstractProjects> company = null;
            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProjectsSelectById, param, commandType: CommandType.StoredProcedure);
                company = task.Read<SuccessResult<AbstractProjects>>().SingleOrDefault();
                company.Item = task.Read<Projects>().SingleOrDefault();
            }
            return company;
        }

        public override SuccessResult<AbstractProjects> ProjectsUpsert(AbstractProjects abstractProjects)
        {
            SuccessResult<AbstractProjects> company = null;
            var param = new DynamicParameters();
            param.Add("@Id", abstractProjects.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Name", abstractProjects.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Description", abstractProjects.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CompanyContactId", abstractProjects.CompanyContactId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StatusId", abstractProjects.StatusId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StartDate", abstractProjects.StartDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add("@EndDate", abstractProjects.EndDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProjectsUpsert, param, commandType: CommandType.StoredProcedure);
                company = task.Read<SuccessResult<AbstractProjects>>().SingleOrDefault();
                company.Item = task.Read<Projects>().SingleOrDefault();
            }
            return company;
        }
        public override bool ProjectsDelete(int Id)
        {
            bool users = false;
            var param = new DynamicParameters();
            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.ProjectsDelete, param, commandType: CommandType.StoredProcedure);
                users = task.SingleOrDefault<bool>();
            }
            return users;
        }

    }
}
