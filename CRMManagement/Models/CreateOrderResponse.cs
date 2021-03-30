using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMManagement.Models
{
    public class CreateOrderResponse
    {
        public double cash_pickups_count { get; set; }
        public int package_count { get; set; }
        public string upload_wbn { get; set; }
        public int replacement_count { get; set; }
        public int pickups_count { get; set; }
        public List<Package> packages { get; set; }
        public double cash_pickups { get; set; }
        public int cod_count { get; set; }
        public bool success { get; set; }
        public int prepaid_count { get; set; }
        public double cod_amount { get; set; }
    }

    public class Package
    {
        public string status { get; set; }
        public string client { get; set; }
        public object sort_code { get; set; }
        public List<string> remarks { get; set; }
        public string waybill { get; set; }
        public double cod_amount { get; set; }
        public string payment { get; set; }
        public bool serviceable { get; set; }
        public string refnum { get; set; }
    }
}