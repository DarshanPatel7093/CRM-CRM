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
    public abstract class AbstractOpportunitiesServices : AbstractBaseService
    {
        public abstract PagedList<AbstractOpportunities> OpportunitiesSelectAll(PageParam pageParam, string Search = "", int CompanyContactId = 0, int AssignedUserId = 0, int StatusId = 0, string StartDate = "", string EndDate = "", int UserId = 0, int DateType = 0);

        public abstract SuccessResult<AbstractOpportunities> OpportunitiesSelectById(int id);

        public abstract SuccessResult<AbstractOpportunities> OpportunitiesUpsert(AbstractOpportunities abstractOpportunities);

        public abstract bool OpportunitiesDelete(int Id);

        public abstract PagedList<AbstractOpportunities> OpportunitiesSelectAllForAsignUser();

        public abstract PagedList<AbstractOpportunities> OpportunitiesSelectAllForAsignUserForFilter();


    }
}
