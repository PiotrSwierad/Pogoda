namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SmallWeathers", "Weather_ID", "dbo.Weathers");
            DropIndex("dbo.SmallWeathers", new[] { "Weather_ID" });
            DropTable("dbo.SmallWeathers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SmallWeathers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WeatherEntryId = c.Int(nullable: false),
                        Temperature = c.Double(nullable: false),
                        Humidity = c.Int(nullable: false),
                        DateOfEntry = c.DateTime(nullable: false),
                        Weather_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.SmallWeathers", "Weather_ID");
            AddForeignKey("dbo.SmallWeathers", "Weather_ID", "dbo.Weathers", "ID");
        }
    }
}
