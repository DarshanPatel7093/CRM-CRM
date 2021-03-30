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
    public abstract class AbstractUsersServices : AbstractBaseService
    {
        public abstract SuccessResult<AbstractUsers> UserLogin(AbstractUsers abstractUsers, int Type);

        public abstract PagedList<AbstractUsers> UsersSelectAll(PageParam pageParam, string Search = "", int RoleId = 0);

        public abstract SuccessResult<AbstractUsers> UsersSelectById(int id);

        public abstract SuccessResult<AbstractUsers> UsersUpsert(AbstractUsers abstractUsers);

        public abstract bool UserDelete(int Id);

        public abstract SuccessResult<AbstractUsers> DashboardDetails(int UserId);
    }
}
