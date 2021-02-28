using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Minitwit.Entities;
using Newtonsoft.Json;
using Repos;

namespace mvp
{
    public class MiniMain : IMiniMain
    {
        public User User { get; set; }
        public IEnumerable<string> FlashedMessages { get; set; }
        public IEnumerable<UserMessageDTO> UserMessageDTO { get; set; }
        public string URL { get; }
        public string APIURL { get; }

        private readonly System.Security.Cryptography.MD5 _md5 = System.Security.Cryptography.MD5.Create();
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public MiniMain(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;

            URL = _navigationManager.BaseUri;
            APIURL = "http://159.89.213.38:5010/";

            User = new User{ user_id = -1 };

            UserMessageDTO = new List<UserMessageDTO>();
            // Timeline();
        }

        public string MD5Hasher(string toBeHashed)
        {
            byte[] emailBytes = Encoding.UTF8.GetBytes(toBeHashed.Trim().ToLower());
            byte[] hashedEmail = _md5.ComputeHash(emailBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedEmail.Length; i++)
            {
                builder.Append(hashedEmail[i].ToString("X2"));
            }

            return builder.ToString().Trim().ToLower();
        }

        public string Url_for(string name)
        {
            switch (name)
            {
                case "timeline":
                    return URL + "";

                case "public timeline":
                    return URL + "public";

                case "logout":
                    return URL + "logout";

                case "register":
                    return URL + "register";
                
                case "login":
                    return URL + "login";
            }

            return "";
        }

        public string UrlForUser(string username)
        {
            return URL + username;
        }

        public string UrlForUnfollow(string username)
        {
            return URL + username + "/unfollow";
        }

        public string UrlForFollow(string username)
        {
            return URL +  username + "/follow";
        }

        public string GravatarUrl(string email, int size=80)
        {
            return "http://www.gravatar.com/avatar/" + 
                    MD5Hasher(email) +
                    "?d=identicon&s=" +
                    size;
        }

        public async Task<IEnumerable<UserMessageDTO>> Timeline()
        {
            if (User != null && User.user_id >= 0) 
            {
                var response = await _httpClient.GetAsync
                (
                    APIURL + "msgs/" + User.username + "/follows"
                );

                var content = await response.Content.ReadAsStringAsync();

                UserMessageDTO = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserMessageDTO>>
                (
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return UserMessageDTO;
            }
            else
            {
                return new List<UserMessageDTO>();
            }
        }

        public async Task<IEnumerable<UserMessageDTO>> PublicTimeline()
        {
            var response = await _httpClient.GetAsync(APIURL + "msgs/");
            var content = await response.Content.ReadAsStringAsync();

            UserMessageDTO = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserMessageDTO>>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return UserMessageDTO;
        }

        public async Task<IEnumerable<UserMessageDTO>> UserTimeline(string username)
        {
            var response = await _httpClient.GetAsync(APIURL + "msgs/" + username);
            var content = await response.Content.ReadAsStringAsync();

            UserMessageDTO = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserMessageDTO>>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return UserMessageDTO;
        }

        public async Task AddUserToDB(string username, string email, string password)
        {
            var userToDB = new User
            {
                username = username,
                email = email,
                pwd = MD5Hasher(password)    
            };
            
            var json = JsonConvert.SerializeObject(userToDB);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(APIURL + "register/", data);
        }

        public async Task AddMessageToDB(string text)
        {
            var messageObj = new
            {
                content = text
            };

            var json = JsonConvert.SerializeObject(messageObj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(APIURL + "msgs/" + User.username, data);
        }

        public async Task Login(string username)
        {
            var response = await _httpClient.GetAsync(APIURL + "user/" + username);
            var content = await response.Content.ReadAsStringAsync();

            var user = System.Text.Json.JsonSerializer.Deserialize<User>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            User = user;
        }

        public async Task<User> GetUserFromUsername(string username)
        {
            var response = await _httpClient.GetAsync(APIURL + "user/" + username);
            var content = await response.Content.ReadAsStringAsync();

            var user = System.Text.Json.JsonSerializer.Deserialize<User>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return user;
        }

        public async Task<bool> IsFollowed(string username1, string username2)
        {
            var response = await _httpClient.GetAsync(APIURL + "fllws/" + username1 + "/" + username2);
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

            await _httpClient.PostAsync(APIURL + "fllws/" + user + "/" + userToUnfollow, data);
        }

        public async Task UnfollowUser(string user, string userToFollow)
        {
            Console.WriteLine(APIURL + "fllws/" + user + "/" + userToFollow);
            
            var response = await _httpClient.DeleteAsync(APIURL + "fllws/" + user + "/" + userToFollow);

            Console.WriteLine(response);
        }
    }
}
