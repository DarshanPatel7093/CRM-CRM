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
    public class CompanyDao : AbstractCompanyDao
    {
        public override PagedList<AbstractCompany> CompanySelectAll(PageParam pageParam, string Search = "")
        {
            PagedList<AbstractCompany> users = new PagedList<AbstractCompany>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortBy", pageParam.SortBy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortDirection", pageParam.SortDirection, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CompanySelectAll, param, commandType: CommandType.StoredProcedure);
                users.Values.AddRange(task.Read<Company>());
                users.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return users;
        }

        public override SuccessResult<AbstractCompany> CompanySelectById(int id)
        {
            SuccessResult<AbstractCompany> company = null;
            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CompanySelectById, param, commandType: CommandType.StoredProcedure);
                company = task.Read<SuccessResult<AbstractCompany>>().SingleOrDefault();
                company.Item = task.Read<Company>().SingleOrDefault();
            }
            return company;
        }

        public override SuccessResult<AbstractCompany> CompanyUpsert(AbstractCompany abstractCompany)
        {
            SuccessResult<AbstractCompany> company = null;
            var param = new DynamicParameters();
            param.Add("@Id", abstractCompany.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Name", abstractCompany.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Address1", abstractCompany.Address1, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Address2", abstractCompany.Address2, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Address3", abstractCompany.Address3, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Phone", abstractCompany.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Fax", abstractCompany.Fax, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CompanyUpsert, param, commandType: CommandType.StoredProcedure);
                company = task.Read<SuccessResult<AbstractCompany>>().SingleOrDefault();
                company.Item = task.Read<Company>().SingleOrDefault();
            }
            return company;
        }
        public override bool CompanyDelete(int Id)
        {
            bool users = false;
            var param = new DynamicParameters();
            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@LoginUserId", ProjectSession.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.CompanyDelete, param, commandType: CommandType.StoredProcedure);
                users = task.SingleOrDefault<bool>();
            }
            return users;
        }

    }
}
