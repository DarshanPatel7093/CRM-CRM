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
    public abstract class AbstractCompanyContactsServices : AbstractBaseService
    {
        public abstract PagedList<AbstractCompanyContacts> CompanyContactsSelectAll(PageParam pageParam, string Search = "", int CompanyId = 0);

        public abstract SuccessResult<AbstractCompanyContacts> CompanyContactsSelectById(int id);

        public abstract SuccessResult<AbstractCompanyContacts> CompanyContactsUpsert(AbstractCompanyContacts abstractCompanyContacts);

        public abstract bool CompanyContactsDelete(int Id);

    }
}
