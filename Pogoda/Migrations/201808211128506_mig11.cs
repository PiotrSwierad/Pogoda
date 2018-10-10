namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Weathers", "WeatherHistory_ID", "dbo.WeatherHistories");
            DropForeignKey("dbo.WeatherHistories", "Weather_ID", "dbo.Weathers");
            DropForeignKey("dbo.Weathers", "WeatherHistory_ID1", "dbo.WeatherHistories");
            DropIndex("dbo.Weathers", new[] { "WeatherHistory_ID" });
            DropIndex("dbo.Weathers", new[] { "WeatherHistory_ID1" });
            DropIndex("dbo.WeatherHistories", new[] { "Weather_ID" });
            DropColumn("dbo.Weathers", "WeatherHistory_ID");
            DropColumn("dbo.Weathers", "WeatherHistory_ID1");
            DropTable("dbo.WeatherHistories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WeatherHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Weather_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Weathers", "WeatherHistory_ID1", c => c.Int());
            AddColumn("dbo.Weathers", "WeatherHistory_ID", c => c.Int());
            CreateIndex("dbo.WeatherHistories", "Weather_ID");
            CreateIndex("dbo.Weathers", "WeatherHistory_ID1");
            CreateIndex("dbo.Weathers", "WeatherHistory_ID");
            AddForeignKey("dbo.Weathers", "WeatherHistory_ID1", "dbo.WeatherHistories", "ID");
            AddForeignKey("dbo.WeatherHistories", "Weather_ID", "dbo.Weathers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Weathers", "WeatherHistory_ID", "dbo.WeatherHistories", "ID");
        }
    }
}
