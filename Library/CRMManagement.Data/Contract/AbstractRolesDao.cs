using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMManagement.Common;
using CRMManagement.Common.Paging;
using CRMManagement.Entities.Contract;

namespace CRMManagement.Data.Contract
{
   public abstract class AbstractRolesDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractRoles> RolesAllDropdown();


    }
}
