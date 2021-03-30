using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMManagement.Models
{
    public class CreateOrderRequest
    {
        public PickupLocation pickup_location { get; set; }
        public List<Shipment> shipments { get; set; }
    }

    public class PickupLocation
    {
        public string pin { get; set; }
        public string add { get; set; }
        public string phone { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string name { get; set; }
    }

    public class Shipment
    {
        public string return_name { get; set; }
        public string return_pin { get; set; }
        public string return_city { get; set; }
        public string return_phone { get; set; }
        public string return_add { get; set; }
        public string return_state { get; set; }
        public string return_country { get; set; }
        public string order { get; set; }
        public string phone { get; set; }
        public string products_desc { get; set; }
        public string cod_amount { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string seller_inv_date { get; set; }
        public string order_date { get; set; }
        public string total_amount { get; set; }
        public string seller_add { get; set; }
        public string seller_cst { get; set; }
        public string add { get; set; }
        public string seller_name { get; set; }
        public string seller_inv { get; set; }
        public string seller_tin { get; set; }
        public string pin { get; set; }
        public string quantity { get; set; }
        public string payment_mode { get; set; }
        public string state { get; set; }
        public string city { get; set; }
    }
}