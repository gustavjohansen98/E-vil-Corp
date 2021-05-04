using Minitwit.Entities;
using System.Collections.Generic;

namespace EvilClient.ViewModels
{
    public interface IUserState
    {
        User User { get; set; }
        
        void Clear();
    }
}