using CRMManagement.Common;

namespace CRMManagement.Entities.Contract
{
    public abstract class AbstractStatusDesc : BaseModel
    {
        public int Id { get; set; }
        public string StatusDesc { get; set; }
    }
}
