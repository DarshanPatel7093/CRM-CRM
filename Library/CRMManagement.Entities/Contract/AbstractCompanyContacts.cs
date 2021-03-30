using CRMManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMManagement.Entities.Contract
{
    public abstract class AbstractCompanyContacts : BaseModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string FullName { get; set; }
        public string DepartField { get; set; }
    }
}
