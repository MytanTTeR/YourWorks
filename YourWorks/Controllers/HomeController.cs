﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourWorks.Models;

namespace YourWorks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var collections = (new AchivementContext()).AchivementCollections.ToList();

            return View(collections);
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
    }
}