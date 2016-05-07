namespace Isdg.IsdgMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Meeting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetings", "Href", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meetings", "Href");
        }
    }
}
