using System;
using System.IO;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Minitwit.Entities;
using EvilAPI;

namespace EvilAPI.Tests
{

    public class MinitwitContextTest : DbContext, IMinitwitContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Follower> Follower { get; set; }
        public DbSet<Message> Message { get; set; }

        public MinitwitContextTest()
        {
            Database.EnsureCreated();
        }

        public MinitwitContextTest(DbContextOptions<MinitwitContextTest> options) : base(options)
        {
            // Database.EnsureCreated();
        }



        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //     => optionsBuilder.UseNpgsql(GetConnectionString.GetPsqlDbClusterConnectionString());
            
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetConnectionString.GetPsqlDbClusterConnectionString());
            }
            else
            {
                var _connection = new SqliteConnection("Filename=:memory:");
                _connection.Open();
                optionsBuilder.UseSqlite(_connection);
                // Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(k => k.username)
                .IsUnique();

            modelBuilder.Entity<Follower>()
                .HasKey(c => new { c.who_id, c.whom_id });

            modelBuilder.Entity<Message>();

        }
    }
}
