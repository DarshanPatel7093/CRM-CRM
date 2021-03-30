using CRMManagement.Common;

namespace CRMManagement.Entities.Contract
{
    public abstract class AbstractRoles : BaseModel
    {
        public int Id { get; set; }
        public string Roles { get; set; }
    }
}
