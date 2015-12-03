namespace YourWorks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AchivementRates", "PhotoAchivement_ID", c => c.Int());
            AddColumn("dbo.AchivementRates", "TextAchivement_ID", c => c.Int());
            CreateIndex("dbo.AchivementRates", "PhotoAchivement_ID");
            CreateIndex("dbo.AchivementRates", "TextAchivement_ID");
            AddForeignKey("dbo.AchivementRates", "PhotoAchivement_ID", "dbo.PhotoAchivements", "ID");
            AddForeignKey("dbo.AchivementRates", "TextAchivement_ID", "dbo.TextAchivements", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AchivementRates", "TextAchivement_ID", "dbo.TextAchivements");
            DropForeignKey("dbo.AchivementRates", "PhotoAchivement_ID", "dbo.PhotoAchivements");
            DropIndex("dbo.AchivementRates", new[] { "TextAchivement_ID" });
            DropIndex("dbo.AchivementRates", new[] { "PhotoAchivement_ID" });
            DropColumn("dbo.AchivementRates", "TextAchivement_ID");
            DropColumn("dbo.AchivementRates", "PhotoAchivement_ID");
        }
    }
}
