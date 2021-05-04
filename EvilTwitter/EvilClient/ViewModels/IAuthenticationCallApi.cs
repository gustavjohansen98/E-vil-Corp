using System.Net;
using System.Threading.Tasks;
using Minitwit.Entities;

namespace EvilClient.ViewModels 
{
    public interface IAuthenticationCallApi
    {
        Task<User> GetUserFromUsername(string username);

        Task<bool> SignIn(string username);
        
        Task<HttpStatusCode> SignUp(string username, string email, string password);

        bool ValidateEmail (string email);

        bool ValidatePassword (string password);

        string GeneratePasswardValidationErrorMessage(string password);
    }
}