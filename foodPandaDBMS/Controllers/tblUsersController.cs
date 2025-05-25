using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using foodPandaDBMS.Models;

namespace foodPandaDBMS.Controllers
{
    public class tblUsersController : Controller
    {
        private dbfoodpandaEntities db = new dbfoodpandaEntities();

        // GET: tblUsers
        public ActionResult Index()
        {
            return View(db.tblUsers.ToList());
        }

        // GET: tblUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
                return HttpNotFound();

            return View(tblUser);
        }

        // GET: tblUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,UserEmail,UserNIC,UserPassword,Address_HouseNo,Address_City,Address_Status,Address_PostalCode")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.tblUsers.Add(tblUser);
                db.SaveChanges();

                Session["UserID"] = tblUser.UserID;
                Session["UserName"] = tblUser.UserName;

                return RedirectToAction("Index", "Foods");
            }

            return View(tblUser);
        }

        // GET: tblUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
                return HttpNotFound();

            return View(tblUser);
        }

        // POST: tblUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,UserEmail,UserNIC,UserPassword,Address_HouseNo,Address_City,Address_Status,Address_PostalCode")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = "🎉 Your profile has been updated successfully!";
                return RedirectToAction("Index", "Foods");
            }
            return View(tblUser);
        }

        // GET: tblUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
                return HttpNotFound();

            return View(tblUser);
        }

        // POST: tblUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            db.tblUsers.Remove(tblUser);
            db.SaveChanges();

            TempData["Success"] = "🗑️ Your account has been deleted successfully. We hope to see you again!";
            Session.Clear();
            return RedirectToAction("Login", "tblUsers");
        }

        // GET: tblUsers/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: tblUsers/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tblUser user, string loginType)
        {
            if (loginType == "admin")
            {
                if (user.UserEmail == "admin" && user.UserPassword == "123")
                {
                    Session["Admin"] = "true";
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.Error = "Invalid Admin Credentials!";
                    return View();
                }
            }
            else
            {
                var loginUser = db.tblUsers.FirstOrDefault(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword);

                if (loginUser != null)
                {
                    Session["UserID"] = loginUser.UserID;
                    Session["UserName"] = loginUser.UserName;
                    return RedirectToAction("Index", "Foods");
                }
                else
                {
                    ViewBag.Error = "Invalid Email or Password.";
                    return View();
                }
            }
        }

        public ActionResult Dashboard()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "tblUsers");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
