namespace YourWorks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AchivementRates", "PhotoAchivement_ID", "dbo.PhotoAchivements");
            DropForeignKey("dbo.AchivementRates", "TextAchivement_ID", "dbo.TextAchivements");
            DropIndex("dbo.AchivementRates", new[] { "PhotoAchivement_ID" });
            DropIndex("dbo.AchivementRates", new[] { "TextAchivement_ID" });
            DropColumn("dbo.AchivementRates", "PhotoAchivement_ID");
            DropColumn("dbo.AchivementRates", "TextAchivement_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AchivementRates", "TextAchivement_ID", c => c.Int());
            AddColumn("dbo.AchivementRates", "PhotoAchivement_ID", c => c.Int());
            CreateIndex("dbo.AchivementRates", "TextAchivement_ID");
            CreateIndex("dbo.AchivementRates", "PhotoAchivement_ID");
            AddForeignKey("dbo.AchivementRates", "TextAchivement_ID", "dbo.TextAchivements", "ID");
            AddForeignKey("dbo.AchivementRates", "PhotoAchivement_ID", "dbo.PhotoAchivements", "ID");
        }
    }
}
