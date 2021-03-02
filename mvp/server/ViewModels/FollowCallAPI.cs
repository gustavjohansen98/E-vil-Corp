using mvp.Extensions

namespace EvilApp.ViewModels
{
    class FollowCallAPI : IFollowCallAPI
    {
        private readonly IUserState _userState;
        private readonly HttpClient _httpClient;
        private readonly IUtilViewModel _utils;

        public FollowCallAPI(IUserState userState, IUtilViewModel utils, HttpClient httpClient)
        {
            _userState = userState;
            _utils = utils;
            _httpClient = httpClient;
        }

        public async Task<bool> IsFollowed(string username1, string username2)
        {
            var response = await _httpClient.GetAsync(_utils.APIURL + "fllws/" + username1 + "/" + username2);
            var content = await response.Content.ReadAsStringAsync();

            var IsUserFollowing = System.Text.Json.JsonSerializer.Deserialize<bool>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return IsUserFollowing;
        }

        public async Task FollowUser(string user, string userToUnfollow)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(_utils.APIURL + "fllws/" + user + "/" + userToUnfollow, data);
        }

        public async Task UnfollowUser(string user, string userToFollow)
        {
            Console.WriteLine(_utils.APIURL + "fllws/" + user + "/" + userToFollow);
            
            var response = await _httpClient.DeleteAsync(_utils.APIURL + "fllws/" + user + "/" + userToFollow);

            Console.WriteLine(response);
        }
    }
}
