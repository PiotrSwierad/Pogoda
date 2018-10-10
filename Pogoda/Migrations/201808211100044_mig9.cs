namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeatherHistories",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Weathers", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeatherHistories", "ID", "dbo.Weathers");
            DropIndex("dbo.WeatherHistories", new[] { "ID" });
            DropTable("dbo.WeatherHistories");
        }
    }
}
