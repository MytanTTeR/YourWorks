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
            var model = new PhotoAchivementsView()
            {
                Achivement = photoAchivement,
                Photo = "/Uploads/PhotoAchivements/Photo/" + photoAchivement.PhotoName,
                Rate = Rates.Where(x => x.Type == RateType.Positive).Count() - Rates.Where(x => x.Type == RateType.Negative).Count(),
            };
            
            return View(model);
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
                return View(photoAchivement);
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
