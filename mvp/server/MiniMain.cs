using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Minitwit.Entities;
using Repos;

namespace mvp
{
    public class MiniMain : IMiniMain
    {
        public User User { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public string URL { get; }

        private readonly IMessageRepository _messageRepo;

        public MiniMain(IMessageRepository messageRepo)
        {
            _messageRepo = messageRepo;

            URL = "http://localhost:5000/";

            User = new User{ ID = 1, username = "" };

            // Messages = new List<Message>();
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

        public IEnumerable<Message> Timeline()
        {
            Messages = _messageRepo.GetAllMessageFromUser(User.ID);
            return Messages;
        }
    }
}