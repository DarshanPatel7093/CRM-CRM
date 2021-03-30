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
    public class OpportunitiesServices : AbstractOpportunitiesServices
    {
        private AbstractOpportunitiesDao abstractOpportunitiesDao;

        public OpportunitiesServices (AbstractOpportunitiesDao abstractOpportunitiesDao)
        {
            this.abstractOpportunitiesDao = abstractOpportunitiesDao;
        }
        public override PagedList<AbstractOpportunities> OpportunitiesSelectAll(PageParam pageParam, string Search = "", int CompanyContactId = 0, int AssignedUserId = 0, int StatusId = 0, string StartDate = "", string EndDate = "", int UserId = 0, int DateType = 0)
        {
            return this.abstractOpportunitiesDao.OpportunitiesSelectAll(pageParam, Search,CompanyContactId,AssignedUserId,StatusId, StartDate, EndDate, UserId ,DateType);
        }

        public override SuccessResult<AbstractOpportunities> OpportunitiesSelectById(int id)
        {
            return this.abstractOpportunitiesDao.OpportunitiesSelectById(id);
        }

        public override SuccessResult<AbstractOpportunities> OpportunitiesUpsert(AbstractOpportunities abstractOpportunities)
        {
            return this.abstractOpportunitiesDao.OpportunitiesUpsert(abstractOpportunities);
        }
        public override bool OpportunitiesDelete(int Id)
        {
            return this.abstractOpportunitiesDao.OpportunitiesDelete(Id);
        }

        public override PagedList<AbstractOpportunities> OpportunitiesSelectAllForAsignUser()
        {
            return this.abstractOpportunitiesDao.OpportunitiesSelectAllForAsignUser();
        }

        public override PagedList<AbstractOpportunities> OpportunitiesSelectAllForAsignUserForFilter()
        {
            return this.abstractOpportunitiesDao.OpportunitiesSelectAllForAsignUserForFilter();
        }

    }
}
