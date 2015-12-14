using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourWorks.Core.AchivementFactory;
using YourWorks.Models;

namespace YourWorks.Core
{
    public class Utilities
    {
        AchivementContext db;
        AchivementFactoryManager manager;

        public Utilities(AchivementContext context)
        {
            this.db = context;
            this.manager = new AchivementFactoryManager(db);
        }

        public int AchivementCount(AchivementCollection collection)
        {
            return manager.GetFactory(collection).GetCount();
        }

        public ItemViewModel ItemCreateAchivement(AchivementCollection collection)
        {
            var factory = manager.GetFactory(collection);
            return new ItemViewModel()
            {
                Name = "Добавить достижение",
                ImageClass = factory.PreviewClass,
                Redirect = new RedirectViewModel()
                {
                    Action = "Create",
                    Controller = GetFactory(collection).Controller,
                    Model = new
                    {
                        id = collection.ID
                    }
                }
            };
        }

        public IEnumerable<ItemViewModel> ItemDetailsAchivements(AchivementCollection collection)
        {
            var factory = GetFactory(collection);
            foreach (var achivement in factory.GetAchivements())
            {
                yield return new ItemViewModel()
                {
                    Name = achivement.Name,
                    ImageClass = factory.PreviewClass,
                    Redirect = new RedirectViewModel()
                    {
                        Action = "Details",
                        Controller = factory.Controller,
                        Model = new
                        {
                            id = achivement.ID,
                        }
                    }
                };
            }
        }

        public FolderViewModel CreateFolder()
        {
            return new FolderViewModel()
            {
                Name = "Добавить коллекцию",
                FolderClass = "folder add-folder",
                Redirect = new RedirectViewModel()
                {
                    Action = "Create",
                    Controller = "AchivementCollections"
                }
            };
        }

        public IEnumerable<FolderViewModel> ViewFolders(AchivementCollection[] achivementCollection)
        {
            string type = "";
            foreach (var collection in achivementCollection)
            {
                if (AchivementCount(collection) != 0) type = "folder file-folder";
                else type = "folder empty-folder";
                
                yield return new FolderViewModel()
                {
                    Name = collection.Name,
                    FolderClass = type,
                    Redirect = new RedirectViewModel()
                    {
                        Action = "Collection",
                        Controller = "Account",
                        Model = new
                        {
                            id = collection.ID,
                        }
                    }
                };
            }
        }

        IAchivementFactory GetFactory(AchivementCollection collection)
        {
            return manager.GetFactory(collection);
        }
    }
}