namespace NidTid.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Report22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reports", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Reports", "UserId", "dbo.Users");
            DropIndex("dbo.Reports", new[] { "ProjectId" });
            DropIndex("dbo.Reports", new[] { "UserId" });
            RenameColumn(table: "dbo.Reports", name: "ProjectId", newName: "ProjectId");
            RenameColumn(table: "dbo.Reports", name: "UserId", newName: "UserId");
            AddForeignKey("dbo.Reports", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reports", "UserId", "dbo.Users", "Id", cascadeDelete: false);
            CreateIndex("dbo.Reports", "ProjectId");
            CreateIndex("dbo.Reports", "UserId");
            DropColumn("dbo.Reports", "Km");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "Km", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropIndex("dbo.Reports", new[] { "UserId" });
            DropIndex("dbo.Reports", new[] { "ProjectId" });
            DropForeignKey("dbo.Reports", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reports", "ProjectId", "dbo.Projects");
            CreateIndex("dbo.Reports", "UserId");
            CreateIndex("dbo.Reports", "ProjectId");
            AddForeignKey("dbo.Reports", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Reports", "ProjectId", "dbo.Projects", "Id");
        }
    }
}
