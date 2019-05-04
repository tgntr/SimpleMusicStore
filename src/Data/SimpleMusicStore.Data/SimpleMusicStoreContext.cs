using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data.Configurations;
using SimpleMusicStore.Data.Models;

namespace SimpleMusicStore.Data
{
    public class SimpleMusicStoreContext : IdentityDbContext<SimpleUser>
    {
        public SimpleMusicStoreContext(DbContextOptions<SimpleMusicStoreContext> options)
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

        public DbSet<Video> Videos { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<RecordOrder> RecordOrders { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfiguration(new ArtistUserConfiguration());

            builder.ApplyConfiguration(new LabelUserConfiguration());

            builder.ApplyConfiguration(new RecorcUserConfiguration());

            builder.ApplyConfiguration(new RecordOrderConfiguration());

            builder.ApplyConfiguration(new OrderConfiguration());
        }


    }
}
