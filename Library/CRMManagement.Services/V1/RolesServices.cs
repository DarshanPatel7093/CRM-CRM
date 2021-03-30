using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMManagement.Common;
using CRMManagement.Common.Paging;
using CRMManagement.Data.Contract;
using CRMManagement.Entities.Contract;
using CRMManagement.Services.Contract;

namespace CRMManagement.Services.V1
{
    public class RolesServices : AbstractRolesServices
    {
        private AbstractRolesDao abstractTruckManagementDao;

        public RolesServices(AbstractRolesDao abstractTruckManagementDao)
        {
            this.abstractTruckManagementDao = abstractTruckManagementDao;
        }

        public override PagedList<AbstractRoles> RolesAllDropdown()
        {
            return this.abstractTruckManagementDao.RolesAllDropdown();
        }


    }
}
