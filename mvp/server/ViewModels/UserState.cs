using Minitwit.Entities;
using System.Collections.Generic;

namespace mvp.ViewModels
{
    public class UserState : IUserState
    {
        public User User { get; set; }

        public UserState()
        {
            User = new User { user_id = -1 };
        }
    }
}