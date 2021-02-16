using System;
using Xunit;
using Repos;
using System.Linq;
using Minitwit.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DB.Tests
{
    public class MessageRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly MinitwitContext _context;
        private readonly IMessageRepository _repo;


        public MessageRepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory");
            _connection.Open();
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

        [Fact]
        public void Add_1_message_to_empty_DB_returns_list_size_2()
        {
            //Given
            var message = new Message{ author_ID = 1, text = "This is the message" };
        
            //When
            _repo.AddMessage(message);

            var listOfMessages = _repo.GetAllMessageFromUser(1);
        
            //Then
            Assert.Equal(2, listOfMessages.ToList().Count);
        }

        public void Dispose()
        {
            _connection.Dispose();
            _context.Dispose();
        }
    }
}