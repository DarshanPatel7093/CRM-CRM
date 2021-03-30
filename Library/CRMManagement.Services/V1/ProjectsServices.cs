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
    public class ProjectsServices : AbstractProjectsServices
    {
        private AbstractProjectsDao abstractProjectsDao;

        public ProjectsServices(AbstractProjectsDao abstractProjectsDao)
        {
            this.abstractProjectsDao = abstractProjectsDao;
        }

        public override PagedList<AbstractProjects> ProjectsSelectAll(PageParam pageParam, string Search = "", int CompanyContactId = 0, int StatusId = 0, string StartDate = "", string EndDate = "", int DateType = 0)
        {
            return this.abstractProjectsDao.ProjectsSelectAll(pageParam, Search, CompanyContactId,StatusId,StartDate,EndDate,DateType);
        }

        public override SuccessResult<AbstractProjects> ProjectsSelectById(int id)
        {
            return this.abstractProjectsDao.ProjectsSelectById(id);
        }

        public override SuccessResult<AbstractProjects> ProjectsUpsert(AbstractProjects abstractProjects)
        {
            return this.abstractProjectsDao.ProjectsUpsert(abstractProjects);
        }

        public override bool ProjectsDelete(int Id)
        {
            return this.abstractProjectsDao.ProjectsDelete(Id);
        }


    }
}
