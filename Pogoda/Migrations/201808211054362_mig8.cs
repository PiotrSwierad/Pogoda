namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Weathers", "WeatherHistory_ID", "dbo.WeatherHistories");
            DropIndex("dbo.Weathers", new[] { "WeatherHistory_ID" });
            DropColumn("dbo.Weathers", "WeatherHistory_ID");
            DropTable("dbo.WeatherHistories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WeatherHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Weathers", "WeatherHistory_ID", c => c.Int());
            CreateIndex("dbo.Weathers", "WeatherHistory_ID");
            AddForeignKey("dbo.Weathers", "WeatherHistory_ID", "dbo.WeatherHistories", "ID");
        }
    }
}
