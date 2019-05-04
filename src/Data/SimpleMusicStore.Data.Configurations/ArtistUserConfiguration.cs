using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMusicStore.Data.Models;

namespace SimpleMusicStore.Data.Configurations
{
    public class ArtistUserConfiguration : IEntityTypeConfiguration<ArtistUser>
    {
        public void Configure(EntityTypeBuilder<ArtistUser> builder)
        {
            builder
                .HasKey(au => new { au.ArtistId, au.UserId });

            builder
                .HasOne(au => au.Artist)
                .WithMany(a => a.Followers)
                .HasForeignKey(au => au.ArtistId);

            builder
                .HasOne(au => au.User)
                .WithMany(u => u.FollowedArtists)
                .HasForeignKey(au => au.UserId);
        }
    }
}
