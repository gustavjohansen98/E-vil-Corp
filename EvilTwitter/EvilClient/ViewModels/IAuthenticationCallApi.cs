using System.Threading.Tasks;
using Minitwit.Entities;

namespace EvilClient.ViewModels 
{
    public interface IAuthenticationCallApi
    {
        Task<User> GetUserFromUsername(string username);

        Task<bool> SignIn(string username);

        Task SignOut();
        
        Task SignUp();
    }
}