using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Isdg.Core.Data;

namespace Isdg.Data.Mapping
{
    public class TextsMap : EntityTypeConfiguration<Text>
    {
        public TextsMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Key).IsRequired();
            Property(t => t.Value).IsRequired();
            Property(t => t.UserId).IsRequired();            
            ToTable("Texts");
        }
    }
}
