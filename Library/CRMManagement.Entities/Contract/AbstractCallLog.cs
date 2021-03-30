using CRMManagement.Common;

namespace CRMManagement.Entities.Contract
{
    public abstract class AbstractCallLog : BaseModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string CallDate { get; set; }
        public string CallLogType { get; set; }
        public string CallLogTypeName { get; set; }
        public string ContactName { get; set; }
        public string Notes { get; set; }
        public string FollowUpDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Type { get; set; }
        public string TaskName { get; set; }
        public int CallLogTypeCount { get; set; }
    }
}
