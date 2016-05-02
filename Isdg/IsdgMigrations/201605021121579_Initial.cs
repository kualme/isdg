namespace Isdg.IsdgMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        Host = c.String(nullable: false),
                        Port = c.Int(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        EnableSsl = c.Boolean(nullable: false),
                        UseDefaultCredentials = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IP = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Place = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.String(nullable: false),
                        MeetingType = c.Int(nullable: false),
                        IsIsdgMeeting = c.Boolean(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IP = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IP = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
            DropTable("dbo.Meetings");
            DropTable("dbo.EmailAccounts");
        }
    }
}
