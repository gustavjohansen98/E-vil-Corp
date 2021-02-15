using Microsoft.EntityFrameworkCore;
using Minitwit.Entities;

namespace DB.Tests
{
    public class DBTestContext : MinitwitContext
    {
        public DBTestContext(DbContextOptions<MinitwitContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, username = "user1", email = "user1@test.com", pw_hash = "hash" },
                new User { ID = 2, username = "user2", email = "user2@test.com", pw_hash = "hash" }
            );

            modelBuilder.Entity<Message>().HasData(
                new Message { ID = 1, author_ID = 1, text = "" }
            );
        }
    }
}