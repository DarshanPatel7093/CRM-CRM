using CRMManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMManagement.Entities.Contract
{
    public abstract class AbstractUsers : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string EmailAddress { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int OpportunitiesNumber { get; set; }
        public int OpportunitiesTaskNumber { get; set; }
        public int ProjectsNumber { get; set; }
    }
}
