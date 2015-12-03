using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourWorks.Models.AchivementCollections
{
    public class AchivementCollectionsDetails
    {
        public ItemViewModel Create { get; set; }
        public IEnumerable<ItemViewModel> Items { get; set; }
    }
}