using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourWorks.Models;

namespace YourWorks.Core.AchivementFactory
{
    public class PhotoCollectionFactory : IAchivementFactory
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
            get { return "PhotoAchivements"; }
        }
        public string PreviewClass
        {
            get { return "achive photo"; }
        }

        public IEnumerable<AbstractAchivement> GetAchivements()
        {
            return GetPhotoAchivements().ToArray<AbstractAchivement>();
        }
        public int GetCount()
        {
            return GetPhotoAchivements().Count();
        }

        IEnumerable<PhotoAchivement> GetPhotoAchivements()
        {
            return db.PhotoAchivements.Where(x => x.AchivementCollectionID == Collection.ID);
        }
    }
}