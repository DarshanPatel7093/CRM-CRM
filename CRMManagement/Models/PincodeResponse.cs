using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMManagement.Models
{
    public class PincodeResponse
    {
        public List<DeliveryCode> delivery_codes { get; set; }
    }

    public class PostalCode
    {
        public string district { get; set; }
        public double pin { get; set; }
        public double? max_amount { get; set; }
        public string pre_paid { get; set; }
        public string cash { get; set; }
        public string pickup { get; set; }
        public string repl { get; set; }
        public string cod { get; set; }
        public string country_code { get; set; }
        public string sort_code { get; set; }
        public string is_oda { get; set; }
        public string state_code { get; set; }
        public double? max_weight { get; set; }
    }

    public class DeliveryCode
    {
        public PostalCode postal_code { get; set; }
    }
}