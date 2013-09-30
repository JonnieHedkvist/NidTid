namespace NidTid.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vehicle22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Description", c => c.String());
            DropColumn("dbo.Vehicles", "PersNr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "PersNr", c => c.String());
            DropColumn("dbo.Vehicles", "Description");
        }
    }
}
