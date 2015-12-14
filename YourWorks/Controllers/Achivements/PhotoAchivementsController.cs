using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YourWorks.Models;
using YourWorks.Core;
using YourWorks.Models.PhotoAchivements;
using Microsoft.AspNet.Identity;

namespace YourWorks.Controllers.Achivements
{
    public class PhotoAchivementsController : Controller
    {
        private AchivementContext db = new AchivementContext();

        // GET: PhotoAchivements
        public ActionResult Details(int? id)
        {
            var userID = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAchivement photoAchivement = db.PhotoAchivements.Find(id);

            if (photoAchivement == null)
            {
                return HttpNotFound();
            }

            var Rates = db.AchivementRates.Where(x => x.AchivementID == photoAchivement.ID && x.AchivementType == AchivementTypes.Photo);
            var Collection = db.AchivementCollections.Find(photoAchivement.AchivementCollectionID);
            ViewBag.Rate = Rates.Where(x => x.Type == RateType.Positive).Count() - Rates.Where(x => x.Type == RateType.Negative).Count();
            ViewBag.UserRate = db.UserRates.Where(x => x.UserID == Collection.UserID).Count();
            ViewBag.IsRated = Rates.Where(x => x.UserID == userID).Count() != 0;

            return View(photoAchivement);
        }

        public ActionResult Rate(int id, RateType type)
        {
            var userID = User.Identity.GetUserId();
            var new_rate = new AchivementRate()
            {
                AchivementID = id,
                AchivementType = AchivementTypes.Photo,
                Type = type,
                UserID = userID
            };
            db.AchivementRates.Add(new_rate);
            db.SaveChanges();
            var rates = db.AchivementRates.Where(x => x.AchivementID == id && x.AchivementType == AchivementTypes.Photo);
            var rate = rates.Where(x => x.Type == RateType.Positive).Count() - rates.Where(x => x.Type == RateType.Negative).Count();
            ViewBag.Rate = rate;
            ViewBag.IsRated = true;
            return PartialView("Rate");
        }

        // GET: PhotoAchivements/Create
        public ActionResult Create(int? id)
        {
            return View(new PhotoAchivement() { AchivementCollectionID = (int)id });
        }

        // POST: PhotoAchivements/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhotoAchivement photoAchivement, HttpPostedFileBase photo)
        {
            AchivementCollection c = db.AchivementCollections.Find(photoAchivement.AchivementCollectionID);
            if (ModelState.IsValid && photo != null && c.UserID == User.Identity.GetUserId() && c.AchivementType == AchivementTypes.Photo)
            {
                photoAchivement.PhotoName = Download.SaveFile(photo, "~/Uploads/PhotoAchivements/Photo/");
                db.PhotoAchivements.Add(photoAchivement);
                db.SaveChanges();
                return RedirectToAction("Collection", "Account", new { id = photoAchivement.AchivementCollectionID });
            }

            return View(photoAchivement);
        }

        // GET: PhotoAchivements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAchivement photoAchivement = db.PhotoAchivements.Find(id);
            if (photoAchivement == null)
            {
                return HttpNotFound();
            }
            return View(photoAchivement);
        }

        // POST: PhotoAchivements/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhotoAchivement photoAchivement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photoAchivement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photoAchivement);
        }

        // GET: PhotoAchivements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAchivement photoAchivement = db.PhotoAchivements.Find(id);
            if (photoAchivement == null)
            {
                return HttpNotFound();
            }
            return View(photoAchivement);
        }

        // POST: PhotoAchivements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhotoAchivement photoAchivement = db.PhotoAchivements.Find(id);
            db.PhotoAchivements.Remove(photoAchivement);
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
