using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourWorks.Models;

namespace YourWorks.Core.AchivementFactory
{
    public class TextCollectionFactory : IAchivementFactory
    {
        public AchivementContext db { get; set; }
        public AchivementCollection Collection { get; set; }

        public string ActionDetails
        {
            get { return "Details"; }
        }
        public string ActionEdit
        {
            get { return "Edit"; }
        }
        public string ActionCreate
        {
            get { return "Create"; }
        }
        public string ActionDelete
        {
            get { return "Delete"; }
        }
        public string Controller
        {
            get { return "TextAchivements"; }
        }

        public IEnumerable<AbstractAchivement> GetAchivements()
        {
            return GetTextAchivements().ToArray<AbstractAchivement>();
        }
        public int GetCount()
        {
            return GetTextAchivements().Count();
        }

        IEnumerable<TextAchivement> GetTextAchivements()
        {
            return db.TextAchivements.Where(x => x.AchivementCollectionID == Collection.ID);
        }
    }
}