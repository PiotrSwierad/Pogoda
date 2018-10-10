namespace Pogoda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weathers", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Weathers", "UserId");
        }
    }
}
