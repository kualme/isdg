namespace Isdg.IsdgMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExecutiveBoardMember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExecutiveBoardMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Href = c.String(),
                        StartYear = c.String(nullable: false),
                        EndYear = c.String(),
                        IsPresident = c.Boolean(nullable: false),
                        IsDead = c.Boolean(nullable: false),
                        UserId = c.String(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IP = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExecutiveBoardMembers");
        }
    }
}
