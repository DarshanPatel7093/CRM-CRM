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
    public class ProjectsTimeTrackingServices : AbstractProjectsTimeTrackingServices
    {
        private AbstractProjectsTimeTrackingDao abstractProjectsTimeTrackingDao;

        public ProjectsTimeTrackingServices(AbstractProjectsTimeTrackingDao abstractProjectsTimeTrackingDao)
        {
            this.abstractProjectsTimeTrackingDao = abstractProjectsTimeTrackingDao;
        }

        public override PagedList<AbstractProjectsTimeTracking> ProjectTimeTrackingSelectAll(PageParam pageParam, string Search = "", int ProjectId = 0, int AssignedUserId = 0, string StartDate = "", string EndDate = "", int DateType = 0)
        {
            return this.abstractProjectsTimeTrackingDao.ProjectTimeTrackingSelectAll(pageParam, Search, ProjectId, AssignedUserId,StartDate, EndDate,DateType);
        }

        public override SuccessResult<AbstractProjectsTimeTracking> ProjectTimeTrackingSelectById(int id)
        {
            return this.abstractProjectsTimeTrackingDao.ProjectTimeTrackingSelectById(id);
        }

        public override SuccessResult<AbstractProjectsTimeTracking> ProjectTimeTrackingUpsert(AbstractProjectsTimeTracking abstractProjectsTimeTracking)
        {
            return this.abstractProjectsTimeTrackingDao.ProjectTimeTrackingUpsert(abstractProjectsTimeTracking);
        }

        public override bool ProjectTimeTrackingDelete(int Id)
        {
            return this.abstractProjectsTimeTrackingDao.ProjectTimeTrackingDelete(Id);
        }

    }
}
