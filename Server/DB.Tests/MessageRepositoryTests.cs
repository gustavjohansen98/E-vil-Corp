using System;
using Xunit;
using Minitwit.Entities;
using Repos;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DB.Tests
{
    public class MessageRepositoryTests
    {
        private readonly SqliteConnection _connection;
        private readonly MinitwitContext _context;
        private readonly IMessageRepository _repo;


        public MessageRepositoryTests()
        {
            // _connection = new SqliteConnection("DataSource=:memory:");
            // _connection.Open();
            var builder = new DbContextOptionsBuilder<MinitwitContext>().UseSqlite(_connection);
            _context = new DBTestContext(builder.Options);
            _context.Database.EnsureCreated();
            _repo = new MessageRepository(_context);
        }

        [Fact]
        public void Given_user_id_1_returns_non_empty_list()
        {
            var listOfMessages = _repo.GetAllMessageFromUser(1);

            Assert.NotEmpty(listOfMessages);
        }
    }
}