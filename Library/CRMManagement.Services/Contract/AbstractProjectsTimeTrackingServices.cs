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
    public abstract class AbstractProjectsTimeTrackingServices : AbstractBaseService
    {
        public abstract PagedList<AbstractProjectsTimeTracking> ProjectTimeTrackingSelectAll(PageParam pageParam, string Search = "", int ProjectId = 0, int AssignedUserId = 0, string StartDate = "", string EndDate = "", int DateType = 0);

        public abstract SuccessResult<AbstractProjectsTimeTracking> ProjectTimeTrackingSelectById(int id);

        public abstract SuccessResult<AbstractProjectsTimeTracking> ProjectTimeTrackingUpsert(AbstractProjectsTimeTracking abstractProjectsTimeTracking);

        public abstract bool ProjectTimeTrackingDelete(int Id);


    }
}
