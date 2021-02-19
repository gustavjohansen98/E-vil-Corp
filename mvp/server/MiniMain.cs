using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Minitwit.Entities;
using Repos;

namespace mvp
{
    public class MiniMain : IMiniMain
    {
        public User User { get; set; }
        public IEnumerable<string> FlashedMessages { get; set; }
        public IEnumerable<UserMessageDTO> UserMessageDTO { get; set; }
        public string URL { get; }

        private readonly System.Security.Cryptography.MD5 _md5 = System.Security.Cryptography.MD5.Create();
        private readonly IMessageRepository _messageRepo;
        private readonly IUserRepository _userRepo;

        public MiniMain(IMessageRepository messageRepo, IUserRepository userRepo)
        {
            _messageRepo = messageRepo;
            _userRepo = userRepo;

            URL = "https://localhost:5001/";

            User = new User{ user_id = -1 };

            UserMessageDTO = new List<UserMessageDTO>();
            // Timeline();
        }

        private string MD5Hasher(string toBeHashed)
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

        public IEnumerable<UserMessageDTO> Timeline()
        {
            if (User != null && User.user_id >= 0) 
            {
                UserMessageDTO = _messageRepo.GetOwnAndFollowedMessages(User.user_id);
                return UserMessageDTO;
            }
            else
            {
                return new List<UserMessageDTO>();
            }
        }

        public IEnumerable<UserMessageDTO> PublicTimeline()
        {
            UserMessageDTO = _messageRepo.GetAllMessages();
            return UserMessageDTO;
        }

        public IEnumerable<UserMessageDTO> UserTimeline(int user_id)
        {
            UserMessageDTO = _messageRepo.GetAllMessageFromUser(user_id);
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

        public void AddMessageToDB(string text)
        {
            _messageRepo.AddMessage(User.user_id, text, DateTime.Now.ToString(), 0);
        }
    }
}