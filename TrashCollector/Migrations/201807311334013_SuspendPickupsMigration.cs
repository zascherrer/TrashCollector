namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SuspendPickupsMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SuspendPickupsUntil", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SuspendPickupsUntil");
        }
    }
}
