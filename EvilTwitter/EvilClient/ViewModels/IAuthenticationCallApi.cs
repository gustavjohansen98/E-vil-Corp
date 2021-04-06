using System.Threading.Tasks;
using Minitwit.Entities;

namespace EvilClient.ViewModels 
{
    public interface IAuthenticationCallApi
    {
        Task<User> GetUserFromUsername(string username);

        Task SignIn();

        Task SignOut();
        
        Task SignUp();
    }
}