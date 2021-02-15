using System;
using System.Net;
using System.Collections.Generic;
using Minitwit.Entities;
using static System.Net.HttpStatusCode;


namespace Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly IMinitwitContext _context;

        public UserRepository(IMinitwitContext context)
        {
            _context = context;
        }

        public HttpStatusCode AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return NoContent;
        }

        public User GetUser(int userID)
        {
            var user = _context.Users.Find(userID);
            if (user == null) 
                return null;

            return user;
        }

    }
}