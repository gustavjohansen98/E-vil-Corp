using Minitwit.Entities;
using System.Collections.Generic;

namespace EvilClient.ViewModels
{
    public class UserState : IUserState
    {
        public User User { get; set; }

        public UserState()
        {
            User = new User { user_id = -1 };
        }

        public void Clear()
        {
            User.user_id = -1;
            User.username = null;
            User.email = null;
            User.pwd = null;
        }
    }
}