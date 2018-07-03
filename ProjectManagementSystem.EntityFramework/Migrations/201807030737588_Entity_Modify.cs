namespace ProjectManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Entity_Modify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modules", "StartTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Modules", "DeliverTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Modules", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "StartTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Projects", "DeliverTime", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "DeliverTime");
            DropColumn("dbo.Projects", "StartTime");
            DropColumn("dbo.Modules", "Level");
            DropColumn("dbo.Modules", "DeliverTime");
            DropColumn("dbo.Modules", "StartTime");
        }
    }
}
