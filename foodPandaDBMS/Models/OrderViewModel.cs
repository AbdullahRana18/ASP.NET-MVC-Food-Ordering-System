using System;

namespace foodPandaDBMS.Models
{
    public class OrderViewModel
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal DeliveryCharges { get; set; } = 200;
        public decimal Total { get; set; }

        // Required for saving order
        public int UserID { get; set; }

        // Payment Method (ID: 1 = Cash on Delivery, 2 = Card Payment)
        public int PaymentMethodID { get; set; } = 1;
    }
}
