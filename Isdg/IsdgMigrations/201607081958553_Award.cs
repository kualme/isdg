namespace Isdg.IsdgMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Award : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Heading = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        PathToFirstPicture = c.String(),
                        PathToSecondPicture = c.String(),
                        UserId = c.String(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IP = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Awards");
        }
    }
}
