namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weathers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Weathers", "ApplicationUser_Id");
            AddForeignKey("dbo.Weathers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Weathers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Weathers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Weathers", "ApplicationUser_Id");
        }
    }
}
