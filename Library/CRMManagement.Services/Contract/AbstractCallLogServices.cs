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
    public abstract class AbstractCallLogServices : AbstractBaseService
    {
        public abstract PagedList<AbstractCallLog> CallLogSelectAll(PageParam pageParam, string Search = "", int TaskId = 0, string StartDate = "", string EndDate = "", int DateType = 0);

        public abstract PagedList<AbstractCallLog> CallLogReportSelectAll(PageParam pageParam, string Search = "", string StartDate = "", string EndDate = "");

        public abstract PagedList<AbstractCallLog> CallLogTypeSelectAll();

        public abstract SuccessResult<AbstractCallLog> CallLogSelectById(int id);

        public abstract SuccessResult<AbstractCallLog> CallLogUpsert(AbstractCallLog abstractCallLog);

        public abstract bool CallLogDelete(int Id);

    }
}
