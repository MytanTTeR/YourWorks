using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YourWorks.Models;
using Microsoft.AspNet.Identity;

namespace YourWorks.Controllers.Achivements
{
    public class TextAchivementsController : Controller
    {
        private AchivementContext db = new AchivementContext();

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextAchivement textAchivement = db.TextAchivements.Find(id);
            if (textAchivement == null)
            {
                return HttpNotFound();
            }
            var Rates = db.AchivementRates.Where(x => x.AchivementID == textAchivement.ID && x.AchivementType == AchivementTypes.Text);
            var Collection = db.AchivementCollections.Find(textAchivement.AchivementCollectionID);
            ViewBag.Rate = Rates.Where(x => x.Type == RateType.Positive).Count() - Rates.Where(x => x.Type == RateType.Negative).Count();
            ViewBag.UserRate = db.UserRates.Where(x => x.UserID == Collection.UserID).Count();
            return View(textAchivement);
        }
        public ActionResult Rate(int id, RateType type)
        {
            var userID = User.Identity.GetUserId();
            var new_rate = new AchivementRate()
            {
                AchivementID = id,
                AchivementType = AchivementTypes.Text,
                Type = type,
                UserID = userID
            };
            db.AchivementRates.Add(new_rate);
            db.SaveChanges();
            var rates = db.AchivementRates.Where(x => x.AchivementID == id && x.AchivementType == AchivementTypes.Text);
            var rate = rates.Where(x => x.Type == RateType.Positive).Count() - rates.Where(x => x.Type == RateType.Negative).Count();
            ViewBag.Rate = rate;
            ViewBag.IsRated = true;
            return PartialView("Rate");
        }

        // GET: TextAchivements/Create
        public ActionResult Create(int? id)
        {
            return View(new TextAchivement() { AchivementCollectionID = (int)id });
        }

        // POST: TextAchivements/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TextAchivement textAchivement)
        {
            AchivementCollection c = db.AchivementCollections.Find(textAchivement.AchivementCollectionID);

            if (ModelState.IsValid && c.UserID == User.Identity.GetUserId() && c.AchivementType == AchivementTypes.Text)
            {
                db.TextAchivements.Add(textAchivement);
                db.SaveChanges();
                return RedirectToAction("Collection", "Account", new { id = textAchivement.AchivementCollectionID });
            }

            return View(textAchivement);
        }

        // GET: TextAchivements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextAchivement textAchivement = db.TextAchivements.Find(id);
            if (textAchivement == null)
            {
                return HttpNotFound();
            }
            return View(textAchivement);
        }

        // POST: TextAchivements/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TextAchivement textAchivement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(textAchivement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(textAchivement);
        }

        // GET: TextAchivements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextAchivement textAchivement = db.TextAchivements.Find(id);
            if (textAchivement == null)
            {
                return HttpNotFound();
            }
            return View(textAchivement);
        }

        // POST: TextAchivements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TextAchivement textAchivement = db.TextAchivements.Find(id);
            db.TextAchivements.Remove(textAchivement);
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
