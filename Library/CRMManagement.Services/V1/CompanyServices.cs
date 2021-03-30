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
    public class CompanyServices : AbstractCompanyServices
    {
        private AbstractCompanyDao abstractCompanyDao;

        public CompanyServices(AbstractCompanyDao abstractCompanyDao)
        {
            this.abstractCompanyDao = abstractCompanyDao;
        }

        public override PagedList<AbstractCompany> CompanySelectAll(PageParam pageParam, string Search = "")
        {
            return this.abstractCompanyDao.CompanySelectAll(pageParam, Search);
        }

        public override SuccessResult<AbstractCompany> CompanySelectById(int id)
        {
            return this.abstractCompanyDao.CompanySelectById(id);
        }

        public override SuccessResult<AbstractCompany> CompanyUpsert(AbstractCompany abstractCompany)
        {
            return this.abstractCompanyDao.CompanyUpsert(abstractCompany);
        }
        public override bool CompanyDelete(int Id)
        {
            return this.abstractCompanyDao.CompanyDelete(Id);
        }

    }
}
