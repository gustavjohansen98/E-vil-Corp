using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Minitwit.Entities;
using static System.Net.HttpStatusCode;
using Microsoft.EntityFrameworkCore;

namespace Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly IMinitwitContext _context;

        public UserRepository(IMinitwitContext context)
        {
            _context = context;
        }

        public IEnumerable<User> RawSQLQuery(string query)
        {
            return _context.Users.FromSqlRaw(query).ToList();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.AsEnumerable();
        }

        public HttpStatusCode AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return NoContent;
        }

        public User GetUserFromID(int userID)
        {
            var user = _context.Users.Find(userID);
            if (user == null) 
                return null;

            return user;
        }

        public int GetUserIDFromUsername(string username)
        {
            var user = _context.Users
                            .Where(u => u.username == $"{username}")
                            .FirstOrDefault<User>();
            if (user == null)
                return -1;

            return user.ID;
        }

    }
}