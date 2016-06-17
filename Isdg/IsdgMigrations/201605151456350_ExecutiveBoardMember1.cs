namespace Isdg.IsdgMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExecutiveBoardMember1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExecutiveBoardMembers", "Workplace", c => c.String());
            AddColumn("dbo.ExecutiveBoardMembers", "Email", c => c.String());
            AddColumn("dbo.ExecutiveBoardMembers", "IsFormer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExecutiveBoardMembers", "IsFormer");
            DropColumn("dbo.ExecutiveBoardMembers", "Email");
            DropColumn("dbo.ExecutiveBoardMembers", "Workplace");
        }
    }
}
