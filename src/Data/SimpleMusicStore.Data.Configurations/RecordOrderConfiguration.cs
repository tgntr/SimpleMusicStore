using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMusicStore.Data.Models;

namespace SimpleMusicStore.Data.Configurations
{
    public class RecordOrderConfiguration : IEntityTypeConfiguration<RecordOrder>
    {
        public void Configure(EntityTypeBuilder<RecordOrder> builder)
        {
            builder
                .HasKey(ro => new { ro.RecordId, ro.OrderId });

            builder
                .HasOne(ro => ro.Record)
                .WithMany(r => r.Orders)
                .HasForeignKey(ro => ro.RecordId);

            builder
                .HasOne(ro => ro.Order)
                .WithMany(u => u.Items)
                .HasForeignKey(ro => ro.OrderId);
        }
    }
}
