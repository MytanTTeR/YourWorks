using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using YourWorks.Core;
using YourWorks.Models;
using YourWorks.Models.AchivementCollections;

namespace YourWorks.Controllers
{
    public class AchivementCollectionsController : Controller
    {
        private AchivementContext db = new AchivementContext();
        private Utilities utility;

        public AchivementCollectionsController() : base()
        {
            this.utility = new Utilities(db);
        }

        public ActionResult ViewList()
        {
            string id = User.Identity.GetUserId();
            AchivementCollection[] collections = db.AchivementCollections.Where(x => x.UserID == id).ToArray();
            var model = new AchivementCollectionsViewList()
            {
                Items = utility.ViewFolders(collections),
                Create = utility.CreateFolder()
            };
            return View(model);
        }

        //public ActionResult AchievmentRedirect(int? id, AchivementTypes type, string actionType)
        //{
        //    RedirectViewModel model;

        //    switch (actionType)
        //    {
        //        case "AddAchivement":
                    
        //            model = utility.RedirectCreateAchivement(collection)
        //        case "Details":

        //        case "Edit":

        //        case "Delete":

        //        default:
        //    }

        //    return View(utility.RedirectCreateAchivement(db.AchivementCollections.Find(id)));
        //}

        // GET: AchivementCollections/Details/5
        public ActionResult Details(int? id)
        {
            AchivementCollection collection = db.AchivementCollections.Find(id);
            var model = new AchivementCollectionsDetails()
            {
                Items = utility.ItemsDetailsAchivements(collection),
                Create = utility.ItemCreateAchivement(collection)
            };
            return View(model);
        }

        // GET: AchivementCollections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AchivementCollections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AchivementCollection achivementCollection)
        {
            if (ModelState.IsValid)
            {
                achivementCollection.UserID = User.Identity.GetUserId();
                db.AchivementCollections.Add(achivementCollection);
                db.SaveChanges();
                return View();
            }

            return View(achivementCollection);
        }

        // GET: AchivementCollections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AchivementCollection achivementCollection = db.AchivementCollections.Find(id);
            if (achivementCollection == null)
            {
                return HttpNotFound();
            }
            return View(achivementCollection);
        }

        // POST: AchivementCollections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AchivementCollection achivementCollection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(achivementCollection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(achivementCollection);
        }

        // GET: AchivementCollections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AchivementCollection achivementCollection = db.AchivementCollections.Find(id);
            if (achivementCollection == null)
            {
                return HttpNotFound();
            }
            return View(achivementCollection);
        }

        // POST: AchivementCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AchivementCollection achivementCollection = db.AchivementCollections.Find(id);
            db.AchivementCollections.Remove(achivementCollection);
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
