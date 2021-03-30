using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMManagement.Common
{
    
    public enum Month
    {
        January = 1,

        February = 2,

        March = 3,

        April = 4,

        May = 5,

        June = 6,

        July = 7,

        August = 8,

        September = 9,

        October = 10,

        November = 11,

        December = 12,
    }

    public enum LeaveType
    {
        [Description("Full Day")]
        FullDay = 1,

        [Description("First Half")]
        FirstHalf = 2,

        [Description("Second Half")]
        SecondHalf = 3
    }

}
