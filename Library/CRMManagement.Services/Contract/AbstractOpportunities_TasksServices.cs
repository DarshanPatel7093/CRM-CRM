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
    public abstract class AbstractOpportunities_TasksServices : AbstractBaseService
    {
        public abstract PagedList<AbstractOpportunities_Tasks> Opportunities_TasksSelectAll(PageParam pageParam, string Search = "", int OpportunityId = 0, int AssignedUserId = 0, int StatusId = 0, string StartDate = "", string EndDate = "");

        public abstract SuccessResult<AbstractOpportunities_Tasks> Opportunities_TasksSelectById(int id);

        public abstract SuccessResult<AbstractOpportunities_Tasks> Opportunities_TasksUpsert(AbstractOpportunities_Tasks abstractOpportunities_Tasks);

        public abstract bool Opportunities_TasksDelete(int Id);

    }
}
