namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WeatherHistories", "ID", "dbo.Weathers");
            DropIndex("dbo.WeatherHistories", new[] { "ID" });
            DropPrimaryKey("dbo.WeatherHistories");
            AddColumn("dbo.Weathers", "WeatherHistory_ID", c => c.Int());
            AddColumn("dbo.Weathers", "WeatherHistory_ID1", c => c.Int());
            AddColumn("dbo.WeatherHistories", "Weather_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.WeatherHistories", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.WeatherHistories", "ID");
            CreateIndex("dbo.Weathers", "WeatherHistory_ID");
            CreateIndex("dbo.Weathers", "WeatherHistory_ID1");
            CreateIndex("dbo.WeatherHistories", "Weather_ID");
            AddForeignKey("dbo.Weathers", "WeatherHistory_ID", "dbo.WeatherHistories", "ID");
            AddForeignKey("dbo.Weathers", "WeatherHistory_ID1", "dbo.WeatherHistories", "ID");
            AddForeignKey("dbo.WeatherHistories", "Weather_ID", "dbo.Weathers", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeatherHistories", "Weather_ID", "dbo.Weathers");
            DropForeignKey("dbo.Weathers", "WeatherHistory_ID1", "dbo.WeatherHistories");
            DropForeignKey("dbo.Weathers", "WeatherHistory_ID", "dbo.WeatherHistories");
            DropIndex("dbo.WeatherHistories", new[] { "Weather_ID" });
            DropIndex("dbo.Weathers", new[] { "WeatherHistory_ID1" });
            DropIndex("dbo.Weathers", new[] { "WeatherHistory_ID" });
            DropPrimaryKey("dbo.WeatherHistories");
            AlterColumn("dbo.WeatherHistories", "ID", c => c.Int(nullable: false));
            DropColumn("dbo.WeatherHistories", "Weather_ID");
            DropColumn("dbo.Weathers", "WeatherHistory_ID1");
            DropColumn("dbo.Weathers", "WeatherHistory_ID");
            AddPrimaryKey("dbo.WeatherHistories", "ID");
            CreateIndex("dbo.WeatherHistories", "ID");
            AddForeignKey("dbo.WeatherHistories", "ID", "dbo.Weathers", "ID");
        }
    }
}
