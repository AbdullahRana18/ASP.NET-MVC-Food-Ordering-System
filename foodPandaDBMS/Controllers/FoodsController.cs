using System.Linq;
using System.Web.Mvc;
using foodPandaDBMS.Models;

namespace foodPandaDBMS.Controllers
{
    public class FoodsController : Controller
    {
        private dbfoodpandaEntities db = new dbfoodpandaEntities(); // Database context

        // ✅ GET: Foods
        public ActionResult Index()
        {
            // Fetch all food items from the database
            var foods = db.tblFoods.ToList();
            return View(foods); // Send the food items to the view
        }

        // ✅ GET: Foods/Order/5
        public ActionResult Order(int id)
        {
            var food = db.tblFoods.FirstOrDefault(f => f.FoodID == id);
            if (food == null)
            {
                return HttpNotFound();
            }

            // Prepare the model to send to view
            var model = new OrderViewModel
            {
                FoodID = food.FoodID,
                FoodName = food.FoodName,
                Price = food.Price
            };

            return View(model);
        }

        // ✅ POST: Foods/Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                // You can save the order in DB here if needed

                // Pass the same model to Receipt
                return View("Receipt", model);
            }

            return View(model);
        }
    }
}
