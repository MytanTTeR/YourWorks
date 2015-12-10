namespace YourWorks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        ToUserID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PhotoAchivements", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.TextAchivements", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextAchivements", "Views");
            DropColumn("dbo.PhotoAchivements", "Views");
            DropTable("dbo.UserRates");
            DropTable("dbo.FavoriteUsers");
        }
    }
}
