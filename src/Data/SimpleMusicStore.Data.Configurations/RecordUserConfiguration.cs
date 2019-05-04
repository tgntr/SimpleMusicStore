using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMusicStore.Data.Models;

namespace SimpleMusicStore.Data.Configurations
{
    public class RecorcUserConfiguration : IEntityTypeConfiguration<RecordUser>
    {
        public void Configure(EntityTypeBuilder<RecordUser> builder)
        {
            builder
               .HasKey(ru => new { ru.RecordId, ru.UserId });

            builder
                .HasOne(ru => ru.Record)
                .WithMany(r => r.WantedBy)
                .HasForeignKey(ru => ru.RecordId);

            builder
                .HasOne(ru => ru.User)
                .WithMany(u => u.Wantlist)
                .HasForeignKey(ru => ru.UserId);
        }
    }
}
