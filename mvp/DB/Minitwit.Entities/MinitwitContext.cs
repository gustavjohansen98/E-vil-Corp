using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Minitwit.Entities;

namespace Minitwit.Entities
{

    public class MinitwitContext : DbContext, IMinitwitContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Follower> Follower  { get; set; }
        public DbSet<Message> Message { get; set; }

        public MinitwitContext()
        {
            // Database.EnsureCreated();
        }

        public MinitwitContext(DbContextOptions<MinitwitContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=db-postgresql-fra1-03227-do-user-8757435-0.b.db.ondigitalocean.com;Port=25060;Database=defaultdb;User Id=doadmin;Password=vy7a7s367vsvsd2j;SslMode=Require;Trust Server Certificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(k => k.username)
                .IsUnique();

            modelBuilder.Entity<Follower>()
                .HasKey(c => new { c.who_id, c.whom_id});

            modelBuilder.Entity<Message>();

        }
    }
}
