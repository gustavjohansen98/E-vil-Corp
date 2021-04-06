using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Minitwit.Entities;
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

        public Task SignOut()
        {
            throw new System.NotImplementedException();
        }

        public Task SignUp()
        {
            throw new System.NotImplementedException();
        }
    }
}

// public async Task AddUserToDB(string username, string email, string password)
// {
//     var userToDB = new User
//     {
//         username = username,
//         email = email,
//         pwd = MD5Hasher(password)    
//     };

//     var json = JsonConvert.SerializeObject(userToDB);
//     var data = new StringContent(json, Encoding.UTF8, "application/json");

//     await _httpClient.PostAsync(APIURL + "register/", data);
// }

// public async Task Login(string username)
// {
//     var response = await _httpClient.GetAsync(APIURL + "user/" + username);
//     var content = await response.Content.ReadAsStringAsync();

//     var user = System.Text.Json.JsonSerializer.Deserialize<User>
//     (
//         content,
//         new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
//     );

//     _userState.User = user;
// }

// public async Task<User> GetUserFromUsername(string username)
// {
//     var response = await _httpClient.GetAsync(APIURL + "user/" + username);
//     var content = await response.Content.ReadAsStringAsync();

//     var user = System.Text.Json.JsonSerializer.Deserialize<User>
//     (
//         content,
//         new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
//     );

//     return user;
// }