namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Weathers", "WeatherHist_ID", "dbo.WeatherHists");
            DropIndex("dbo.Weathers", new[] { "WeatherHist_ID" });
            DropColumn("dbo.Weathers", "WeatherHist_ID");
            DropTable("dbo.WeatherHists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WeatherHists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Weathers", "WeatherHist_ID", c => c.Int());
            CreateIndex("dbo.Weathers", "WeatherHist_ID");
            AddForeignKey("dbo.Weathers", "WeatherHist_ID", "dbo.WeatherHists", "ID");
        }
    }
}
