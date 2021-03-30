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
    public class CompanyContactsServices : AbstractCompanyContactsServices
    {
        private AbstractCompanyContactsDao abstractCompanyContactsDao;

        public CompanyContactsServices(AbstractCompanyContactsDao abstractCompanyContactsDao)
        {
            this.abstractCompanyContactsDao = abstractCompanyContactsDao;
        }

        public override PagedList<AbstractCompanyContacts> CompanyContactsSelectAll(PageParam pageParam, string Search = "", int CompanyId = 0)
        {
            return this.abstractCompanyContactsDao.CompanyContactsSelectAll(pageParam, Search, CompanyId);
        }

        public override SuccessResult<AbstractCompanyContacts> CompanyContactsSelectById(int id)
        {
            return this.abstractCompanyContactsDao.CompanyContactsSelectById(id);
        }

        public override SuccessResult<AbstractCompanyContacts> CompanyContactsUpsert(AbstractCompanyContacts abstractCompanyContacts)
        {
            return this.abstractCompanyContactsDao.CompanyContactsUpsert(abstractCompanyContacts);
        }
        public override bool CompanyContactsDelete(int Id)
        {
            return this.abstractCompanyContactsDao.CompanyContactsDelete(Id);
        }

    }
}
