using CRMManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMManagement.Entities.Contract
{
    public abstract class AbstractProjects : BaseModel
    {
        public int Id { get; set; }
        public int CompanyContactId { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CompanyContactFullName { get; set; }
        public string StatusText { get; set; }

    }
}
