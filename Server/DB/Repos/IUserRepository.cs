using System.Collections.Generic;
using System.Net;
using Minitwit.Entities;

namespace Repos
{
    public interface IUserRepository
    {
        HttpStatusCode AddUser(User user);

        IEnumerable<User> GetAllUsers();

        User GetUserFromID(int userID);

        int GetUserIDFromUsername(string username);
    }
}