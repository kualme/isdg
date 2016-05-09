namespace Isdg.IsdgMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SendedEmail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SendedEmails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                        UserId = c.String(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IP = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SendedEmails");
        }
    }
}
