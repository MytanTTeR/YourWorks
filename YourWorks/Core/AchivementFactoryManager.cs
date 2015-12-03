using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourWorks.Core.AchivementFactory;
using YourWorks.Models;

namespace YourWorks.Core
{
    class AchivementFactoryManager
    {
        public AchivementContext db;

        public AchivementFactoryManager(AchivementContext db)
        {
            this.db = db;
        }

        public IAchivementFactory GetFactory(AchivementCollection collection)
        {
            switch (collection.AchivementType)
            {
                case AchivementTypes.Photo:
                    return new PhotoCollectionFactory()
                    {
                        db = db,
                        Collection = collection
                    };
                case AchivementTypes.Text:
                    return new TextCollectionFactory()
                    {
                        db = db,
                        Collection = collection
                    };
                default:
                    return new NullCollectionFactory();
            }
        }
        public IAchivementFactory GetFactory(AchivementTypes type)
        {
            switch (type)
            {
                case AchivementTypes.Photo:
                    return new PhotoCollectionFactory
                    {
                        db = db
                    };
                case AchivementTypes.Text:
                    return new TextCollectionFactory
                    {
                        db = db
                    };
                default:
                    return new NullCollectionFactory();
            }
        }
    }
}