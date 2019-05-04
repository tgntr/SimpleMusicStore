using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMusicStore.Data.Models;

namespace SimpleMusicStore.Data.Configurations
{
    public class LabelUserConfiguration : IEntityTypeConfiguration<LabelUser>
    {
        public void Configure(EntityTypeBuilder<LabelUser> builder)
        {
            builder
                .HasKey(lu => new { lu.LabelId, lu.UserId });

            builder
                .HasOne(lu => lu.Label)
                .WithMany(l => l.Followers)
                .HasForeignKey(lu => lu.LabelId);

            builder
                .HasOne(lu => lu.User)
                .WithMany(u => u.FollowedLabels)
                .HasForeignKey(lu => lu.UserId);
        }
    }
}
