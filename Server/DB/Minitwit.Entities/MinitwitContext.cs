using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Minitwit.Entities;

namespace Minitwit.Entities
{

    public class MinitwitContext : DbContext, IMinitwitContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Follower> Followers  { get; set; }

        public DbSet<Message> Messages { get; set; }

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
                var _connection = new SqliteConnection("Filename=:memory");
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

            // var userMock = new User {
            //     ID = 1,
            //     username = "test",
            //     email = "test@mail.com",
            //     pw_hash = "lol"
            // };

            // modelBuilder.Entity<User>().HasData(userMock);
        }
    }
}