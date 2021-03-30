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
    public class CallLogServices : AbstractCallLogServices
    {
        private AbstractCallLogDao abstractCallLogDao;

        public CallLogServices(AbstractCallLogDao abstractCallLogDao)
        {
            this.abstractCallLogDao = abstractCallLogDao;
        }
        public override PagedList<AbstractCallLog> CallLogSelectAll(PageParam pageParam, string Search = "", int TaskId = 0,string StartDate="",string EndDate="", int DateType = 0)
        {
            return this.abstractCallLogDao.CallLogSelectAll(pageParam, Search, TaskId, StartDate, EndDate,DateType);
        }
        public override PagedList<AbstractCallLog> CallLogReportSelectAll(PageParam pageParam, string Search = "",string StartDate="",string EndDate="")
        {
            return this.abstractCallLogDao.CallLogReportSelectAll(pageParam, Search, StartDate, EndDate);
        }
        public override PagedList<AbstractCallLog> CallLogTypeSelectAll()
        {
            return this.abstractCallLogDao.CallLogTypeSelectAll();
        }

        public override SuccessResult<AbstractCallLog> CallLogSelectById(int id)
        {
            return this.abstractCallLogDao.CallLogSelectById(id);
        }

        public override SuccessResult<AbstractCallLog> CallLogUpsert(AbstractCallLog abstractCallLog)
        {
            return this.abstractCallLogDao.CallLogUpsert(abstractCallLog);
        }
        public override bool CallLogDelete(int Id)
        {
            return this.abstractCallLogDao.CallLogDelete(Id);
        }

    }
}
