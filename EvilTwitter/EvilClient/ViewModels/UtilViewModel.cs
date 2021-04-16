using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Minitwit.Entities;
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;

namespace EvilClient.ViewModels
{
    public class UtilViewModel : IUtilViewModel
    {
        public IEnumerable<string> FlashedMessages { get; set; }
        public IEnumerable<UserMessageDTO> UserMessageDTO { get; set; }
        public string URL { get; }
        public string APIURL { get; }

        private readonly System.Security.Cryptography.MD5 _md5 = System.Security.Cryptography.MD5.Create();
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public UtilViewModel(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;

            URL = _navigationManager.BaseUri;
            APIURL = "http://159.89.213.38:5010/";

            UserMessageDTO = new List<UserMessageDTO>();
        }

        public UtilViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string StringToHash(string toBeHashed, HashAlgorithm algorithm)
        {
            var byteConversion = Encoding.UTF8.GetBytes(toBeHashed.Trim().ToLower());
            var hashed = algorithm.ComputeHash(byteConversion);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashed.Length; i++)
            {
                builder.Append(hashed[i].ToString("X2"));
            }

            return builder.ToString().Trim().ToLower();
        }

        public string CurrentPasswordHasher(string passwordToBeHashed)
        {
            return StringToHash(passwordToBeHashed, SHA256.Create());
        }

        /// <summary>
        /// passwordExpected should be hashed already
        /// </summary>
        public bool DoesPasswordMatch(string passwordGiven, string passwordExpected)
        {
            if (StringToHash(passwordGiven, SHA256.Create()) == passwordExpected) return true;

            if (StringToHash(passwordGiven, MD5.Create()) == passwordExpected) return true;
            
            return false;
        }

        public string UrlFor(string name)
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

        public string GravatarUrl(string email, int size = 80)
        {
            return "http://www.gravatar.com/avatar/" +
                    StringToHash(email, MD5.Create()) +
                    "?d=identicon&s=" +
                    size;
        }
    }
}