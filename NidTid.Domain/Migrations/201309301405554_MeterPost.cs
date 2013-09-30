namespace NidTid.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeterPost : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeterPosts", "CurrentMeter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MeterPosts", "currentMeter", c => c.Int(nullable: false));
        }
    }
}
