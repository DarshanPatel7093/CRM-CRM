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
    public class UsersServices : AbstractUsersServices
    {
        private AbstractUsersDao abstractUsersDao;

        public UsersServices (AbstractUsersDao abstractUsersDao)
        {
            this.abstractUsersDao = abstractUsersDao;
        }

        public override SuccessResult<AbstractUsers> UserLogin(AbstractUsers abstractUsers, int Type)
        {
            return this.abstractUsersDao.UserLogin(abstractUsers, Type);
        }
        public override PagedList<AbstractUsers> UsersSelectAll(PageParam pageParam, string Search = "", int RoleId = 0)
        {
            return this.abstractUsersDao.UsersSelectAll(pageParam, Search, RoleId);
        }

        public override SuccessResult<AbstractUsers> UsersSelectById(int id)
        {
            return this.abstractUsersDao.UsersSelectById(id);
        }

        public override SuccessResult<AbstractUsers> UsersUpsert(AbstractUsers abstractUsers)
        {
            return this.abstractUsersDao.UsersUpsert(abstractUsers);
        }
        public override bool UserDelete(int Id)
        {
            return this.abstractUsersDao.UserDelete(Id);
        }
        public override SuccessResult<AbstractUsers> DashboardDetails(int UserId)
        {
            return this.abstractUsersDao.DashboardDetails(UserId);
        }
    }
}
