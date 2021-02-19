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
            Database.EnsureCreated();
        }

        public MinitwitContext(DbContextOptions<MinitwitContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var _connection = new SqliteConnection(@"Data Source=minitwit.db");
                _connection.Open();
                optionsBuilder.UseSqlite(_connection);
                Database.EnsureCreated();
            }

        }

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
