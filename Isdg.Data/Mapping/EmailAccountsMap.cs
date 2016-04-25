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
    class EmailAccountsMap : EntityTypeConfiguration<EmailAccount>
    {
        public EmailAccountsMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AddedDate).IsRequired();
            Property(t => t.DisplayName).IsRequired();
            Property(t => t.Email).IsRequired();
            Property(t => t.EnableSsl).IsRequired();
            Property(t => t.Host).IsRequired();
            Property(t => t.IP).IsRequired();
            Property(t => t.ModifiedDate).IsRequired();
            Property(t => t.Password).IsRequired();
            Property(t => t.Port).IsRequired();
            Property(t => t.UseDefaultCredentials).IsRequired();
            Property(t => t.Username).IsRequired();
            ToTable("EmailAccounts");
        }
    }
}
