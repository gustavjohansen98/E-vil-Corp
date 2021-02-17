using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private readonly IMessageRepository _messageRepo;

        public MiniMain(IMessageRepository messageRepo)
        {
            _messageRepo = messageRepo;

            URL = "https://localhost:5001/";

            // User = new User{ user_id = 1, username = "" };

            UserMessageDTO = new List<UserMessageDTO>();
            Timeline();
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
                    // TODO
                    break;

                case "register":
                    // TODO
                    break;
                
                case "login":
                    // TODO
                    break;
            }

            return "";
        }

        public IEnumerable<UserMessageDTO> Timeline()
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
    }
}