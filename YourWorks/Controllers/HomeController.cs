using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YourWorks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string fileName = Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult Upload(IEnumerable<HttpPostedFileBase> uploads)
        //{
        //    if (uploads != null)
        //    {
        //        foreach (var file in uploads)
        //        {
        //            if (file != null)
        //            {
        //                string fileName = Path.GetFileName(file.FileName);
        //                file.SaveAs(Server.MapPath("~/Files/" + fileName));
        //            }
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}