using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YourWorks.Models
{
    public enum AchivementTypes { Photo, Text }
    public class AchivementContext : DbContext
    {
        public DbSet<AchivementCollection> AchivementCollections { get; set; }
        public DbSet<TextAchivement> TextAchivements { get; set; }
        public DbSet<PhotoAchivement> PhotoAchivements { get; set; }
        public DbSet<AchivementRate> AchivementRates { get; set; }

        public AchivementContext() : base("DefaultConnection") { }
    }

    [Bind(Include = "Name, AchivementType")]
    public class AchivementCollection
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public AchivementTypes AchivementType { get; set; }
    }

    //public interface IAchivement
    //{
    //    int ID { get; set; }
    //    int AchivementCollectionID { get; set; }
    //    AchivementCollection Collection { get; set; }
    //    string Name { get; set; }
    //    string Description { get; set; }
    //    ICollection<AchivementRate> Rates { get; set; }
    //}

    public class AbstractAchivement
    {
        public int ID { get; set; }
        public int AchivementCollectionID { get; set; }
        public AchivementCollection Collection { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    } 

    [Bind(Include = "Name, Description, Text, AchivementCollectionID")]
    public class TextAchivement : AbstractAchivement
    {
        public string Text { get; set; }
    }

    [Bind(Include = "Name, Description, AchivementCollectionID")]
    public class PhotoAchivement : AbstractAchivement
    {
        public string PhotoName { get; set; }
    }

    public enum RateType { Positive, Negative }

    [Bind(Include = "AchivementID, AchivementType, Type")]
    public class AchivementRate
    {
        public int AchivementRateID { get; set; }
        public string UserID { get; set; }
        public int AchivementID { get; set; }
        public AchivementTypes AchivementType { get; set; }
        public RateType Type { get; set; }
    }
}