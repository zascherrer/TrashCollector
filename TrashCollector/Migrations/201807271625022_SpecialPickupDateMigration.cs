namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecialPickupDateMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SpecialPickupDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SpecialPickupDate");
        }
    }
}
