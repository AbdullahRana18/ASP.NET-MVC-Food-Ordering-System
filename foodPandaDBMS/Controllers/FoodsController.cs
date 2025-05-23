using System;
using System.Linq;
using System.Web.Mvc;
using foodPandaDBMS.Models;

namespace foodPandaDBMS.Controllers
{
    public class FoodsController : Controller
    {
        private dbfoodpandaEntities db = new dbfoodpandaEntities();

        // GET: Foods
        public ActionResult Index()
        {
            var foods = db.tblFoods.ToList();
            return View(foods);
        }

        // GET: Foods/Order/5
        public ActionResult Order(int id)
        {
            var food = db.tblFoods.FirstOrDefault(f => f.FoodID == id);
            if (food == null) return HttpNotFound();

            var model = new OrderViewModel
            {
                FoodID = food.FoodID,
                FoodName = food.FoodName,
                Price = food.Price,
                Quantity = 1, // Default quantity
                DeliveryCharges = 200,
                Total = food.Price * 1 + 200 // default total
            };

            return View(model);
        }

        // POST: Foods/Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(OrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int userId = Convert.ToInt32(Session["UserID"]); // Get logged-in user

            // Recalculate Total (security and trust)
            model.Total = (model.Price * model.Quantity) + model.DeliveryCharges;

            // 1. Save Order
            var newOrder = new tblOrder
            {
                UserID = userId,
                Status = "Pending",
                DeliveryAddress = "Dummy Address", // Replace with real address if needed
                OrderDate = DateTime.Now
            };

            db.tblOrders.Add(newOrder);
            db.SaveChanges();

            // 2. Link Food to Order
            var orderFood = new tblOrderFood
            {
                OrderID = newOrder.OrderID,
                FoodID = model.FoodID,
                Quantity = model.Quantity
            };
            db.tblOrderFoods.Add(orderFood);
            db.SaveChanges();

            // 3. Save Payment
            var payment = new tblPayment
            {
                UserID = userId,
                OrderID = newOrder.OrderID,
                PaymentMethodID = model.PaymentMethodID,
                PaymentDate = DateTime.Now,
                Amount = model.Total
            };
            db.tblPayments.Add(payment);
            db.SaveChanges();

            // 4. Show Receipt
            return View("Receipt", model);
        }
    }
}
