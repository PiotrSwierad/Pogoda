namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        CityName = c.String(),
                        CityCountry = c.String(),
                        CurrentTemperature = c.Double(nullable: false),
                        CurrentHumidity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Weathers");
        }
    }
}
