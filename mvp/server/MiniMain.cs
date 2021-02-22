using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
        private readonly IMessageRepository _messageRepo;
        private readonly IUserRepository _userRepo;
        private readonly HttpClient _httpClient;

        public MiniMain(IMessageRepository messageRepo, IUserRepository userRepo, HttpClient httpClient)
        {
            _messageRepo = messageRepo;
            _userRepo = userRepo;
            _httpClient = httpClient;

            URL = "http://localhost:5000/";
            APIURL = "http://localhost:5010/";

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
            return URL + username + "/follow";
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

        public async Task<IEnumerable<UserMessageDTO>> UserTimeline(int user_id)
        {
            var response = await _httpClient.GetAsync(APIURL + "msgs/" + User.username);
            var content = await response.Content.ReadAsStringAsync();

            UserMessageDTO = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserMessageDTO>>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return UserMessageDTO;
        }

        public void AddUserToDB(string username, string email, string password)
        {
            var userToDB = new User
            {
                username = username,
                email = email,
                pw_hash = MD5Hasher(password)    
            };

            _userRepo.AddUser(userToDB);
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

            _messageRepo.AddMessage(User.user_id, text, DateTime.Now, 0);
        }
    }
}