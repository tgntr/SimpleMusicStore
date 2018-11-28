using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Models;

namespace SimpleMusicStore.Data
{
    public class SimpleDbContext : IdentityDbContext<SimpleUser>
    {
        public SimpleDbContext(DbContextOptions<SimpleDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Record> Records { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Label> Labels { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ArtistUser> ArtistUsers { get; set; }

        public DbSet<LabelUser> LabelUsers { get; set; }

        public DbSet<RecordUser> RecordUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ArtistUser>()
                .HasKey(au => new { au.ArtistId, au.UserId });

            builder.Entity<ArtistUser>()
                .HasOne(au => au.Artist)
                .WithMany(a => a.Followers)
                .HasForeignKey(au=>au.ArtistId);

            builder.Entity<ArtistUser>()
                .HasOne(au => au.User)
                .WithMany(u => u.FollowedArtists)
                .HasForeignKey(au => au.UserId);

            builder.Entity<LabelUser>()
                .HasKey(lu => new { lu.LabelId, lu.UserId });

            builder.Entity<LabelUser>()
                .HasOne(lu => lu.Label)
                .WithMany(l => l.Followers)
                .HasForeignKey(lu => lu.LabelId);

            builder.Entity<LabelUser>()
                .HasOne(lu => lu.User)
                .WithMany(u => u.FollowedLabels)
                .HasForeignKey(lu => lu.UserId);

            builder.Entity<RecordUser>()
                .HasKey(ru => new { ru.RecordId, ru.UserId });

            builder.Entity<RecordUser>()
                .HasOne(ru => ru.Record)
                .WithMany(r => r.WantedBy)
                .HasForeignKey(ru => ru.RecordId);

            builder.Entity<RecordUser>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.Wantlist)
                .HasForeignKey(ru => ru.UserId);
            
        }

        
    }
}
