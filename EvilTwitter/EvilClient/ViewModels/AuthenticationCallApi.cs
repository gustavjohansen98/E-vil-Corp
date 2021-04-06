using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Minitwit.Entities;
using Newtonsoft.Json;
using static System.Net.HttpStatusCode;

namespace EvilClient.ViewModels
{
    public class AuthenticationCallApi : IAuthenticationCallApi
    {
        private readonly IUtilViewModel _util;
        private readonly IUserState _userState;
        private readonly HttpClient _httpClient;

        public AuthenticationCallApi(IUtilViewModel utilViewModel, IUserState userState, HttpClient httpClient)
        {
            _util = utilViewModel;
            _userState = userState;
            _httpClient = httpClient;
        }

        public async Task<User> GetUserFromUsername(string username)
        {
            var response = await _httpClient.GetAsync(_util.APIURL + "user/" + username);
            var content = await response.Content.ReadAsStringAsync();

            var user = System.Text.Json.JsonSerializer.Deserialize<User>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return user;
        }

        public async Task<bool> SignIn(string username)
        {
            var response = await _httpClient.GetAsync(_util.APIURL + "user/" + username);

            if (response.StatusCode != OK)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();

            var user = System.Text.Json.JsonSerializer.Deserialize<User>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            _userState.User = user;
            return true;
        }

        public async Task<HttpStatusCode> SignUp(string username, string email, string password)
        {
            var userToDB = new User
            {
                username = username,
                email = email,
                pwd = _util.CurrentPasswordHasher(password)
            };
            
            var json = JsonConvert.SerializeObject(userToDB);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_util.APIURL + "register/", data);

            if (response.StatusCode == NoContent)
            {
                return OK;
            }

            return BadRequest;
        }
    }
}