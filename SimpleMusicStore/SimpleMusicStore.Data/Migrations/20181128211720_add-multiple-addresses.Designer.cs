﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleMusicStore.Data;

namespace SimpleMusicStore.Data.Migrations
{
    [DbContext(typeof(SimpleDbContext))]
    [Migration("20181128211720_add-multiple-addresses")]
    partial class addmultipleaddresses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Street");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("DiscogsId");

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.ArtistUser", b =>
                {
                    b.Property<int>("ArtistId");

                    b.Property<string>("UserId");

                    b.HasKey("ArtistId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ArtistUsers");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArtistId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("DatePosted");

                    b.Property<int?>("LabelId");

                    b.Property<int?>("RecordId");

                    b.Property<int?>("TrackId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("LabelId");

                    b.HasIndex("RecordId");

                    b.HasIndex("TrackId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("DiscogsId");

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.LabelUser", b =>
                {
                    b.Property<int>("LabelId");

                    b.Property<string>("UserId");

                    b.HasKey("LabelId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("LabelUsers");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtistId");

                    b.Property<string>("Description");

                    b.Property<int>("DiscogsId");

                    b.Property<string>("Genre");

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<int>("LabelId");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("LabelId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.RecordUser", b =>
                {
                    b.Property<int>("RecordId");

                    b.Property<string>("UserId");

                    b.HasKey("RecordId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RecordUsers");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.SimpleUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Duration")
                        .IsRequired();

                    b.Property<int>("RecordId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RecordId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.SimpleUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.SimpleUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleMusicStore.Models.SimpleUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.SimpleUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Address", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.SimpleUser", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.ArtistUser", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.Artist", "Artist")
                        .WithMany("Followers")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleMusicStore.Models.SimpleUser", "User")
                        .WithMany("FollowedArtists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Comment", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.Artist")
                        .WithMany("Comments")
                        .HasForeignKey("ArtistId");

                    b.HasOne("SimpleMusicStore.Models.Label")
                        .WithMany("Comments")
                        .HasForeignKey("LabelId");

                    b.HasOne("SimpleMusicStore.Models.Record")
                        .WithMany("Comments")
                        .HasForeignKey("RecordId");

                    b.HasOne("SimpleMusicStore.Models.Track")
                        .WithMany("Comments")
                        .HasForeignKey("TrackId");

                    b.HasOne("SimpleMusicStore.Models.SimpleUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SimpleMusicStore.Models.LabelUser", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.Label", "Label")
                        .WithMany("Followers")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleMusicStore.Models.SimpleUser", "User")
                        .WithMany("FollowedLabels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Record", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.Artist", "Artist")
                        .WithMany("Records")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleMusicStore.Models.Label", "Label")
                        .WithMany("Records")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleMusicStore.Models.RecordUser", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.Record", "Record")
                        .WithMany("WantedBy")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleMusicStore.Models.SimpleUser", "User")
                        .WithMany("Wantlist")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleMusicStore.Models.Track", b =>
                {
                    b.HasOne("SimpleMusicStore.Models.Record", "Record")
                        .WithMany("Tracks")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
