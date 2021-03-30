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
    public class CompanyContactsDao : AbstractCompanyContactsDao
    {
        public override PagedList<AbstractCompanyContacts> CompanyContactsSelectAll(PageParam pageParam, string Search = "",int CompanyId = 0)
        {
            PagedList<AbstractCompanyContacts> users = new PagedList<AbstractCompanyContacts>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CompanyId", CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@SortBy", pageParam.SortBy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortDirection", pageParam.SortDirection, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CompanyContactsSelectAll, param, commandType: CommandType.StoredProcedure);
                users.Values.AddRange(task.Read<CompanyContacts>());
                users.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return users;
        }

        public override SuccessResult<AbstractCompanyContacts> CompanyContactsSelectById(int id)
        {
            SuccessResult<AbstractCompanyContacts> company = null;
            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CompanyContactsSelectById, param, commandType: CommandType.StoredProcedure);
                company = task.Read<SuccessResult<AbstractCompanyContacts>>().SingleOrDefault();
                company.Item = task.Read<CompanyContacts>().SingleOrDefault();
            }
            return company;
        }

        public override SuccessResult<AbstractCompanyContacts> CompanyContactsUpsert(AbstractCompanyContacts abstractCompanyContacts)
        {
            SuccessResult<AbstractCompanyContacts> company = null;
            var param = new DynamicParameters();
            param.Add("@Id", abstractCompanyContacts.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@FName", abstractCompanyContacts.FName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LName", abstractCompanyContacts.LName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Email", abstractCompanyContacts.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CompanyId", abstractCompanyContacts.CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Phone", abstractCompanyContacts.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@DepartField", abstractCompanyContacts.DepartField, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CompanyContactsUpsert, param, commandType: CommandType.StoredProcedure);
                company = task.Read<SuccessResult<AbstractCompanyContacts>>().SingleOrDefault();
                company.Item = task.Read<CompanyContacts>().SingleOrDefault();
            }
            return company;
        }
        public override bool CompanyContactsDelete(int Id)
        {
            bool users = false;
            var param = new DynamicParameters();
            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.CompanyContactsDelete, param, commandType: CommandType.StoredProcedure);
                users = task.SingleOrDefault<bool>();
            }
            return users;
        }

    }
}
