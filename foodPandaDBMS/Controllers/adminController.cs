using System.Linq;
using System.Web.Mvc;
using foodPandaDBMS.Models;

namespace foodPandaDBMS.Controllers
{
    public class AdminController : Controller
    {
        private dbfoodpandaEntities db = new dbfoodpandaEntities();

        // GET: Admin/Stats
        public ActionResult Stats()
        {
            var stats = new AdminStatsViewModel
            {
                TotalUsers = db.tblUsers.Count(),
                TotalFoods = db.tblFoods.Count(),
                TotalSales = db.tblPayments.Sum(p => (decimal?)p.Amount) ?? 0
            };

            return View(stats);
        }
    }
}
