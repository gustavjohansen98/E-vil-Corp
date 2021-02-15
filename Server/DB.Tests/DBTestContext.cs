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
                new User { ID = 2, username = "user2", email = "user2@test.com", pw_hash = "hash" },
                new User { ID = 3, username = "user3", email = "user3@test.com", pw_hash = "hash" },
                new User { ID = 4, username = "user4", email = "user4@test.com", pw_hash = "hash" }
            );

            modelBuilder.Entity<Follower>().HasData(
                new Follower { who_id = 1, whom_id = 2 },
                new Follower { who_id = 2, whom_id = 1 }
            );

            // modelBuilder.Entity<Task>().HasData(
            //     new Task { Id = 1, Title = "title1", Description = "description1", AssignedToId = null, State = New },
            //     new Task { Id = 2, Title = "title2", Description = "description2", AssignedToId = 1, State = Active },
            //     new Task { Id = 3, Title = "title3", Description = "description3", AssignedToId = 2, State = Resolved },
            //     new Task { Id = 4, Title = "title4", Description = "description4", AssignedToId = 1, State = Closed },
            //     new Task { Id = 5, Title = "title5", Description = "description5", AssignedToId = 2, State = Removed }
            // );

            // modelBuilder.Entity<TaskTag>().HasData(
            //     new TaskTag { TaskId = 2, TagId = 1 },
            //     new TaskTag { TaskId = 2, TagId = 2 },
            //     new TaskTag { TaskId = 3, TagId = 1 },
            //     new TaskTag { TaskId = 4, TagId = 2 }
            // );
        }
    }
}