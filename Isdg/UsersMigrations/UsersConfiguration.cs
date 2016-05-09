namespace Isdg.UsersMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class UsersConfiguration : DbMigrationsConfiguration<Isdg.Models.ApplicationDbContext>
    {
        public UsersConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"UsersMigrations";
        }

        protected override void Seed(Isdg.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
