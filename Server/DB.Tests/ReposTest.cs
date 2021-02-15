using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

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
            // var user = new User{

            // };

            var username = "user1";
            var userid = 1;

            var user = _repository.GetUserFromID(userid);

            Assert.Equal(username, user.username);

        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }
    }
}
