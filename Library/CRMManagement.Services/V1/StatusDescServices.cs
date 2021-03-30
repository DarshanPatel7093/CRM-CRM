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
    public class StatusDescServices : AbstractStatusDescServices
    {
        private AbstractStatusDescDao abstractTruckManagementDao;

        public StatusDescServices(AbstractStatusDescDao abstractTruckManagementDao)
        {
            this.abstractTruckManagementDao = abstractTruckManagementDao;
        }

        public override PagedList<AbstractStatusDesc> StatusDescAllDropdown()
        {
            return this.abstractTruckManagementDao.StatusDescAllDropdown();
        }


    }
}
