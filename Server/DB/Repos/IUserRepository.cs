using System.Collections.Generic;
using System.Net;
using Minitwit.Entities;

namespace Repos
{
    public interface IUserRepository
    {
        HttpStatusCode AddUser(User user);

        User GetUser(int userID);
    }
}