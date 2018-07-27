namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmountOwedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AmountOwed", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AmountOwed");
        }
    }
}
