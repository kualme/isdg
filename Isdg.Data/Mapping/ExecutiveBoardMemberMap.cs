using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Isdg.Core.Data;

namespace Isdg.Data.Mapping
{
    public class ExecutiveBoardMemberMap : EntityTypeConfiguration<ExecutiveBoardMember>
    {
        public ExecutiveBoardMemberMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired();
            Property(t => t.Href).IsOptional();            
            Property(t => t.StartYear).IsRequired();
            Property(t => t.EndYear).IsRequired();
            Property(t => t.Workplace).IsOptional();
            Property(t => t.Email).IsOptional();
            Property(t => t.IsFormer).IsRequired();
            Property(t => t.IsPresident).IsRequired();
            Property(t => t.IsDead).IsRequired();
            Property(t => t.UserId).IsRequired();
            ToTable("ExecutiveBoardMembers");
        }
    }
}
