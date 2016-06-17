namespace Isdg.IsdgMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExecutiveBoardMember2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ExecutiveBoardMembers", "EndYear", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExecutiveBoardMembers", "EndYear", c => c.String());
        }
    }
}
