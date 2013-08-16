namespace NidTid.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReprot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "Km", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "Km");
        }
    }
}
