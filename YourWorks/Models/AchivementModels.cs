using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YourWorks.Models
{
    public enum AchivementTypes
    {
        [Display(Name = "Фото")]
        Photo,
        [Display(Name = "Текст")]
        Text
    }
    public enum RateType { Positive, Negative }

    public class AchivementContext : DbContext
    {
        public DbSet<AchivementCollection> AchivementCollections { get; set; }
        public DbSet<TextAchivement> TextAchivements { get; set; }
        public DbSet<PhotoAchivement> PhotoAchivements { get; set; }
        public DbSet<AchivementRate> AchivementRates { get; set; }
        public DbSet<FavoriteUser> FavoriteUsers { get; set; }
        public DbSet<UserRate> UserRates { get; set; }

        public AchivementContext() : base("DefaultConnection") { }
    }

    [Bind(Include = "Name, AchivementType")]
    public class AchivementCollection
    {
        [Key]
        public int ID { get; set; }
        
        public string UserID { get; set; }
        
        [Required]
        [StringLength(50)]
        [DisplayName("Имя коллекции")]
        public string Name { get; set; }
        
        [Required]
        [DisplayName("Тип коллекции")]
        public AchivementTypes AchivementType { get; set; }
    }

    public class AbstractAchivement
    {
        [Key]
        public int ID { get; set; }

        [Column("CollectionID")]
        public int AchivementCollectionID { get; set; }

        [ForeignKey("AchivementCollectionID")]
        public AchivementCollection Collection { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Имя")]
        public string Name { get; set; }
        
        [StringLength(250)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        public int Views { get; set; }
    } 

    [Bind(Include = "Name, Description, Text, AchivementCollectionID")]
    public class TextAchivement : AbstractAchivement
    {
        [Required]
        public string Text { get; set; }
    }

    [Bind(Include = "Name, Description, AchivementCollectionID")]
    public class PhotoAchivement : AbstractAchivement
    {
        [DisplayName("Файл")]
        public string PhotoName { get; set; }
    }

    [Bind(Include = "AchivementID, AchivementType, Type")]
    public class AchivementRate
    {
        [Key]
        public int ID { get; set; }
        
        public string UserID { get; set; }
        
        public int AchivementID { get; set; }
        
        public AchivementTypes AchivementType { get; set; }
        
        public RateType Type { get; set; }
    }

    [Bind(Include = "ID, UserID, Type")]
    public class UserRate
    {
        [Key]
        public int ID { get; set; }

        public string UserID { get; set; }

        public RateType Type { get; set; }
    }

    [Bind(Include = "ID, UserID, ToUserID")]
    public class FavoriteUser
    {
        [Key]
        public int ID { get; set; }

        public string UserID { get; set; }

        public string ToUserID { get; set; }
    }
}