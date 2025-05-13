using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foodPandaDBMS.Models
{
    public class OrderViewModel
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryCharges { get; set; } = 200;
        public decimal Total => Price + DeliveryCharges;

    }
}