namespace ProjectManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(),
                        MemberId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 16, storeType: "nvarchar"),
                        Description = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        TechStack = c.String(nullable: false, maxLength: 16, storeType: "nvarchar"),
                        IsFinished = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.MemberId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamLeaderId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 16, storeType: "nvarchar"),
                        Description = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        IsFinished = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.TeamLeaderId)
                .Index(t => t.TeamLeaderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "TeamLeaderId", "dbo.AbpUsers");
            DropForeignKey("dbo.Modules", "MemberId", "dbo.AbpUsers");
            DropIndex("dbo.Projects", new[] { "TeamLeaderId" });
            DropIndex("dbo.Modules", new[] { "MemberId" });
            DropIndex("dbo.Modules", new[] { "ProjectId" });
            DropTable("dbo.Projects");
            DropTable("dbo.Modules");
        }
    }
}
