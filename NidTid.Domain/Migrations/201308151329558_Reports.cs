namespace NidTid.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Deb = c.Decimal(precision: 18, scale: 2),
                        EjDeb = c.Decimal(precision: 18, scale: 2),
                        Notes = c.String(),
                        Km = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        SalaryId = c.Int(),
                        ProjectId = c.Int(nullable: false),
                        BillId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reports", new[] { "UserId" });
            DropIndex("dbo.Reports", new[] { "ProjectId" });
            DropForeignKey("dbo.Reports", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reports", "ProjectId", "dbo.Projects");
            DropTable("dbo.Reports");
        }
    }
}
