namespace Isdg.IsdgMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Albums1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "IsPublished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "IsPublished");
        }
    }
}
