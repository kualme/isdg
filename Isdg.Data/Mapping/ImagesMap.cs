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
    public class ImagesMap : EntityTypeConfiguration<Image>
    {
        public ImagesMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Path).IsRequired();
            Property(t => t.PathToPreview).IsRequired();
            Property(t => t.Caption).IsOptional();
            ToTable("Images");
        }
    }
}
