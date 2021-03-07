using Minitwit.Entities;
using System.Collections.Generic;

namespace mvp.ViewModels
{
    public interface IUserState
    {
        User User { get; set; }
    }
}