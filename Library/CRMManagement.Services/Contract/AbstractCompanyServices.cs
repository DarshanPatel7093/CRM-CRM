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
    public abstract class AbstractCompanyServices : AbstractBaseService
    {
        public abstract PagedList<AbstractCompany> CompanySelectAll(PageParam pageParam, string Search = "");

        public abstract SuccessResult<AbstractCompany> CompanySelectById(int id);

        public abstract SuccessResult<AbstractCompany> CompanyUpsert(AbstractCompany abstractCompany);

        public abstract bool CompanyDelete(int Id);

    }
}
