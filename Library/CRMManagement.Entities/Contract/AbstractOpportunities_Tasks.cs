using CRMManagement.Common;

namespace CRMManagement.Entities.Contract
{
    public abstract class AbstractOpportunities_Tasks : BaseModel
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        public int AssignedUserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public string FollowupDate { get; set; }
        public string Notes { get; set; }
        public string OpportunityName { get; set; }
        public string AssignedUserName { get; set; }
    }
}
