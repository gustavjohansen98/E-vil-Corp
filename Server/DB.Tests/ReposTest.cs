using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using static System.Net.HttpStatusCode;

using Minitwit.Entities;
using Repos;

namespace DB.Tests
{
    public class ReposTest : IDisposable
    {

        private readonly SqliteConnection _connection;
        private readonly MinitwitContext _context;
        private readonly UserRepository _repository;

        public ReposTest()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            var builder = new DbContextOptionsBuilder<MinitwitContext>().UseSqlite(_connection);
            _context = new DBTestContext(builder.Options);
            _context.Database.EnsureCreated();
            _repository = new UserRepository(_context);
        }

        [Fact]
        public void Test1()
        {
          var test = new Follower();
        }

        [Fact]
        public void Given_userid_returns_user()
        {
            var username = "user1";
            var userid = 1;

            var user = _repository.GetUserFromID(userid);

            Assert.Equal(username, user.username);
        }

        [Fact]
        public void Given_Username_returns_userID()
        {
            var userID = _repository.GetUserFromUsername("user1");

            Assert.Equal(1, userID);
        }

        [Fact]
        public void Given_new_user_AddUser()
        {
            var newUser = new User {
                ID = 3,
                username = "mock",
                email = "test@mail.com",
                pw_hash = "some_hash"
            };

            var statusCode = _repository.AddUser(newUser);
            var insertedUser = _repository.GetUserFromID(newUser.ID);

            Assert.Equal(NoContent, statusCode);
            Assert.Equal(newUser, insertedUser);
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }
    }
}
