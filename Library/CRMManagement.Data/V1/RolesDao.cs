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
    public class RolesDao : AbstractRolesDao
    {
        public override PagedList<AbstractRoles> RolesAllDropdown()
        {
            PagedList<AbstractRoles> TruckManagement = new PagedList<AbstractRoles>();
            var param = new DynamicParameters();
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.RolesAllDropdown, param, commandType: CommandType.StoredProcedure);
                TruckManagement.Values.AddRange(task.Read<Roles>());
                TruckManagement.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return TruckManagement;
        }
    }
}
