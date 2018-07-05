namespace ProjectManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class State_Modify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modules", "State", c => c.Byte(nullable: false));
            AddColumn("dbo.Projects", "State", c => c.Byte(nullable: false));
            DropColumn("dbo.Modules", "IsFinished");
            DropColumn("dbo.Projects", "IsFinished");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "IsFinished", c => c.Boolean(nullable: false));
            AddColumn("dbo.Modules", "IsFinished", c => c.Boolean(nullable: false));
            DropColumn("dbo.Projects", "State");
            DropColumn("dbo.Modules", "State");
        }
    }
}
