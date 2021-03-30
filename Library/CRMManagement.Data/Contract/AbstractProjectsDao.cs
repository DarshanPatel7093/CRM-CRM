using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMManagement.Common;
using CRMManagement.Common.Paging;
using CRMManagement.Entities.Contract;

namespace CRMManagement.Data.Contract
{
   public abstract class AbstractProjectsDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractProjects> ProjectsSelectAll(PageParam pageParam, string Search = "", int CompanyContactId = 0, int StatusId = 0, string StartDate = "", string EndDate = "", int DateType = 0);

        public abstract SuccessResult<AbstractProjects> ProjectsSelectById(int id);

        public abstract SuccessResult<AbstractProjects> ProjectsUpsert(AbstractProjects abstractProjects);

        public abstract bool ProjectsDelete(int Id);


    }
}
