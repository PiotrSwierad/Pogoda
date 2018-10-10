namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmallWeathers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WeatherReportId = c.Int(nullable: false),
                        Temperature = c.Double(nullable: false),
                        Humidity = c.Int(nullable: false),
                        DateOf = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SmallWeathers");
        }
    }
}
