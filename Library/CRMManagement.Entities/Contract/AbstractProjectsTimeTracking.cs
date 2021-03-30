using CRMManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMManagement.Entities.Contract
{
    public abstract class AbstractProjectsTimeTracking : BaseModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int AssignedUserId { get; set; }
        public string WorkDone { get; set; }
        public string WorkDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int TotalHrs { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }

    }
}
