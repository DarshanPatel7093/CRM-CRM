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
    public class Opportunities_TasksServices : AbstractOpportunities_TasksServices
    {
        private AbstractOpportunities_TasksDao abstractOpportunities_TasksDao;

        public Opportunities_TasksServices (AbstractOpportunities_TasksDao abstractOpportunities_TasksDao)
        {
            this.abstractOpportunities_TasksDao = abstractOpportunities_TasksDao;
        }
        public override PagedList<AbstractOpportunities_Tasks> Opportunities_TasksSelectAll(PageParam pageParam, string Search = "", int OpportunityId = 0, int AssignedUserId = 0, int StatusId = 0, string StartDate = "", string EndDate = "")
        {
            return this.abstractOpportunities_TasksDao.Opportunities_TasksSelectAll(pageParam, Search, OpportunityId, AssignedUserId, StatusId,StartDate,EndDate);
        }

        public override SuccessResult<AbstractOpportunities_Tasks> Opportunities_TasksSelectById(int id)
        {
            return this.abstractOpportunities_TasksDao.Opportunities_TasksSelectById(id);
        }

        public override SuccessResult<AbstractOpportunities_Tasks> Opportunities_TasksUpsert(AbstractOpportunities_Tasks abstractOpportunities_Tasks)
        {
            return this.abstractOpportunities_TasksDao.Opportunities_TasksUpsert(abstractOpportunities_Tasks);
        }
        public override bool Opportunities_TasksDelete(int Id)
        {
            return this.abstractOpportunities_TasksDao.Opportunities_TasksDelete(Id);
        }

    }
}
