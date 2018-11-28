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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        
    }
}
