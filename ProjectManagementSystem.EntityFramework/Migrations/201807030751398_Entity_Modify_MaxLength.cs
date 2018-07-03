namespace ProjectManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Entity_Modify_MaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Modules", "Name", c => c.String(nullable: false, maxLength: 32, storeType: "nvarchar"));
            AlterColumn("dbo.Modules", "TechStack", c => c.String(nullable: false, maxLength: 32, storeType: "nvarchar"));
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false, maxLength: 32, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false, maxLength: 16, storeType: "nvarchar"));
            AlterColumn("dbo.Modules", "TechStack", c => c.String(nullable: false, maxLength: 16, storeType: "nvarchar"));
            AlterColumn("dbo.Modules", "Name", c => c.String(nullable: false, maxLength: 16, storeType: "nvarchar"));
        }
    }
}
