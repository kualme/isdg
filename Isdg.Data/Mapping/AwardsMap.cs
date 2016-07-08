using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Isdg.Core.Data;

namespace Isdg.Data.Mapping
{
    public class AwardsMap : EntityTypeConfiguration<Award>
    {
        public AwardsMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Content).IsRequired();
            Property(t => t.Heading).IsRequired();
            Property(t => t.UserId).IsRequired();
            ToTable("Awards");
        }
    }
}
