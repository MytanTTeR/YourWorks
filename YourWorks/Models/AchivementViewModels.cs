using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YourWorks.Models
{
    public class RedirectViewModel
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public object Model { get; set; }
    }

    public class ItemViewModel
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public RedirectViewModel Redirect { get; set; }
    }

    public class FolderViewModel
    {
        public string Name { get; set; }
        public string FolderClass { get; set; }
        public RedirectViewModel Redirect { get; set; }
    }
}