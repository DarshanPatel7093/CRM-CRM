using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMManagement.Common;
using CRMManagement.Common.Paging;
using CRMManagement.Entities.Contract;

namespace CRMManagement.Services.Contract
{
    public abstract class AbstractStatusDescServices : AbstractBaseService
    {
        public abstract PagedList<AbstractStatusDesc> StatusDescAllDropdown();


    }
}
