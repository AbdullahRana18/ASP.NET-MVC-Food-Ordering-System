using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using foodPandaDBMS.Models;

namespace foodPandaDBMS.Controllers
{
    public class tblFoodsController : Controller
    {
        private dbfoodpandaEntities db = new dbfoodpandaEntities();

        // GET: tblFoods
        public ActionResult Index()
        {
            var tblFoods = db.tblFoods.Include(t => t.tblRestaurant);
            return View(tblFoods.ToList());
        }

        // GET: tblFoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblFood tblFood = db.tblFoods.Find(id);
            if (tblFood == null)
                return HttpNotFound();

            return View(tblFood);
        }

        // GET: tblFoods/Create
        public ActionResult Create()
        {
            ViewBag.RestaurantID = new SelectList(db.tblRestaurants, "RestaurantID", "RestaurantName");
            return View();
        }

        // POST: tblFoods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblFood tblFood, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload to root /images folder
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ImageFile.FileName);
                    string folderPath = Server.MapPath("~/images");

                    // Create folder if it doesn't exist
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fullPath = Path.Combine(folderPath, fileName);
                    ImageFile.SaveAs(fullPath);

                    tblFood.ImagePath = "/images/" + fileName;
                }

                db.tblFoods.Add(tblFood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantID = new SelectList(db.tblRestaurants, "RestaurantID", "RestaurantName", tblFood.RestaurantID);
            return View(tblFood);
        }

        // GET: tblFoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblFood tblFood = db.tblFoods.Find(id);
            if (tblFood == null)
                return HttpNotFound();

            ViewBag.RestaurantID = new SelectList(db.tblRestaurants, "RestaurantID", "RestaurantName", tblFood.RestaurantID);
            return View(tblFood);
        }

        // POST: tblFoods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblFood tblFood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantID = new SelectList(db.tblRestaurants, "RestaurantID", "RestaurantName", tblFood.RestaurantID);
            return View(tblFood);
        }

        // GET: tblFoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblFood tblFood = db.tblFoods.Find(id);
            if (tblFood == null)
                return HttpNotFound();

            return View(tblFood);
        }

        // POST: tblFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblFood tblFood = db.tblFoods.Find(id);
            db.tblFoods.Remove(tblFood);
            db.SaveChanges();
            return RedirectToAction("Index");
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
