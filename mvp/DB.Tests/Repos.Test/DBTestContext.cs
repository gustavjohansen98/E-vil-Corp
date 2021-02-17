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
                new User { user_id = 1, username = "user1", email = "user1@test.com", pwd = "hash" },
                new User { user_id = 2, username = "user2", email = "user2@test.com", pwd = "hash" },
                new User { user_id = 3, username = "user3", email = "user3@test.com", pwd = "hash" },
                new User { user_id = 4, username = "user4", email = "user4@test.com", pwd = "hash" }
            );

            modelBuilder.Entity<Follower>().HasData(
                new Follower { who_id = 1, whom_id = 2 },
                new Follower { who_id = 2, whom_id = 1 }
            );

            // modelBuilder.Entity<Message>().HasData(
            //     new Message { ID = 1, author_ID = 1, text = "" }
            // );
        }
    }
}