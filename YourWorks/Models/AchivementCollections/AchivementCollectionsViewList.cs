using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourWorks.Models.AchivementCollections
{
    public class AchivementCollectionsViewList
    {
        public FolderViewModel Create { get; set; }
        public IEnumerable<FolderViewModel> Items { get; set; }
    }
}