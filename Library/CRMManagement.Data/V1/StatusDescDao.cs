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
    public class StatusDescDao : AbstractStatusDescDao
    {
        public override PagedList<AbstractStatusDesc> StatusDescAllDropdown()
        {
            PagedList<AbstractStatusDesc> TruckManagement = new PagedList<AbstractStatusDesc>();
            var param = new DynamicParameters();
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.StatusDescAllDropdown, param, commandType: CommandType.StoredProcedure);
                TruckManagement.Values.AddRange(task.Read<StatusDesc>());
                TruckManagement.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return TruckManagement;
        }
    }
}
