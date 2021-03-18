using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Minitwit.Entities;
using Microsoft.AspNetCore.Components;

namespace mvp.ViewModels
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
            APIURL = "http://localhost:5010/";
            
            UserMessageDTO = new List<UserMessageDTO>();
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
    }
}