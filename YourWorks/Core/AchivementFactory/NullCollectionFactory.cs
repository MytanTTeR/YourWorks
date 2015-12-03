using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourWorks.Models;

namespace YourWorks.Core.AchivementFactory
{
    public class NullCollectionFactory : IAchivementFactory
    {
        public AchivementContext db { get; set; }
        public AchivementCollection Collection { get; set; }

        public string ActionDetails { get { return null; } }
        public string ActionEdit { get { return null; } }
        public string ActionCreate { get { return null; } }
        public string ActionDelete { get { return null; } }
        public string Controller { get { return null; } }

        public IEnumerable<AbstractAchivement> GetAchivements () { return null; }
        public int GetCount() { return 0; }
    }
}