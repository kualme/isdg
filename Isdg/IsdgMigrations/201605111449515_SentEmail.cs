namespace Isdg.IsdgMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SentEmail : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SendedEmails", newName: "SentEmails");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SentEmails", newName: "SendedEmails");
        }
    }
}
