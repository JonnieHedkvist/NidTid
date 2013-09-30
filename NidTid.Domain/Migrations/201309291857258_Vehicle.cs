namespace NidTid.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vehicle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeterPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        currentMeter = c.Int(nullable: false),
                        Notes = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegNr = c.String(),
                        PersNr = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MeterPosts", new[] { "VehicleId" });
            DropIndex("dbo.MeterPosts", new[] { "UserId" });
            DropForeignKey("dbo.MeterPosts", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.MeterPosts", "UserId", "dbo.Users");
            DropTable("dbo.Vehicles");
            DropTable("dbo.MeterPosts");
        }
    }
}
