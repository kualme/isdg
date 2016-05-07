using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Isdg.Core.Data;

namespace Isdg.Data.Mapping
{
    public class MeetingsMap : EntityTypeConfiguration<Meeting>
    {
        public MeetingsMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Title).IsRequired();
            Property(t => t.Place).IsRequired();
            Property(t => t.Href).IsRequired();
            Property(t => t.StartDate).IsRequired();
            Property(t => t.EndDate).IsRequired();
            Property(t => t.MeetingType).IsRequired();
            Property(t => t.IsIsdgMeeting).IsRequired();
            ToTable("Meetings");
        }
    }
}
