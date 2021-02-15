using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Minitwit.Entities;

namespace Minitwit.Entities
{

    public class MinitwitContext : DbContext, IMinitwitContext
    {
        public DbSet<User> Users => throw new NotImplementedException();

        public DbSet<Follower> Followers => throw new NotImplementedException();

        public DbSet<Message> Messages => throw new NotImplementedException();

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
            modelBuilder.Entity<User>();

            modelBuilder.Entity<Follower>()
                .HasKey(c => new { c.who_id, c.whom_id});

            modelBuilder.Entity<Message>();

            // TODO: no foreign keys ?

        }
    }
}