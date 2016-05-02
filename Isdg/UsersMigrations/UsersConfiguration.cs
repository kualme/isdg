namespace Isdg.UsersMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Isdg.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class UsersConfiguration : DbMigrationsConfiguration<Isdg.Models.ApplicationDbContext>
    {
        public UsersConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"UsersMigrations";
        }

        protected override void Seed(Isdg.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(new IdentityRole[] { new IdentityRole("Untrusted"), new IdentityRole("Trusted"), new IdentityRole("Admin") });
            context.Users.AddOrUpdate(new ApplicationUser() { Email = "admin@test.com", EmailConfirmed = true, UserName = "Administrator", PasswordHash = "Admin@123456" });            
        }
    }
}
