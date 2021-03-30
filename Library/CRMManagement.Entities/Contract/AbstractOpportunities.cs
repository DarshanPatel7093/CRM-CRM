using CRMManagement.Common;

namespace CRMManagement.Entities.Contract
{
    public abstract class AbstractOpportunities : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyContactId { get; set; }
        public int StatusId { get; set; }
        public int SuccessRatePercent { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int AssignedUserId { get; set; }
        public string CompanyContactName { get; set; }
        public string AssignedUserName { get; set; }
        public string StatusName { get; set; }
    }
}
