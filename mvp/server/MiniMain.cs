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
        public IEnumerable<UserMessageDTO> UserMessageDTO { get; set; }
        public string URL { get; }

        private readonly System.Security.Cryptography.MD5 _md5 = System.Security.Cryptography.MD5.Create();
        private readonly IMessageRepository _messageRepo;

        public MiniMain(IMessageRepository messageRepo)
        {
            _messageRepo = messageRepo;

            URL = "https://localhost:5001/";

            User = new User{ user_id = -1 };

            UserMessageDTO = new List<UserMessageDTO>();
            // Timeline();
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

        public string UrlForUnfollow(string username)
        {
            return URL + username + "/unfollow";
        }

        public string UrlForFollow(string username)
        {
            return URL + username + "/unfollow";
        }

        public string GravatarUrl(string email, int size=80)
        {
            // http://www.gravatar.com/avatar/%s?d=identicon&s=%d' % \
            // (md5(email.strip().lower().encode('utf-8')).hexdigest(), size)

            byte[] emailBytes = Encoding.UTF8.GetBytes(email.Trim().ToLower());
            byte[] hashedEmail = _md5.ComputeHash(emailBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedEmail.Length; i++)
            {
                builder.Append(hashedEmail[i].ToString("X2"));
            }

            return "http://www.gravatar.com/avatar/" + 
                    builder.ToString().ToLower() +
                    "?d=identicon&s=" +
                    size;
        }

        public IEnumerable<UserMessageDTO> Timeline() // TODO
        {
            if (User != null && User.user_id >= 0) 
            {
                // Messages = _messageRepo.GetAllMessageFromUser(User.user_id);
            }
            else
            {
                return UserMessageDTO = _messageRepo.GetAllMessages();
            }

            return UserMessageDTO;
        }

        public IEnumerable<UserMessageDTO> PublicTimeline()
        {
            UserMessageDTO = _messageRepo.GetAllMessages();
            return UserMessageDTO;
        }

        public IEnumerable<UserMessageDTO> UserTimeline()
        {
            UserMessageDTO = _messageRepo.GetAllMessageFromUser(User.user_id);
            return UserMessageDTO;
        }
    }
}