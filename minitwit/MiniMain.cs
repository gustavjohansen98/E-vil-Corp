using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace minitwit 
{
    public class MiniMain : IMiniMain
    {
        public string User { get; set; }
        public IEnumerable<string> Messages { get; set; }

        public string URL { get; }

        private readonly HttpClient _client;

        public MiniMain(HttpClient client)
        {
            URL = "http://localhost:5000/";
            Messages = new List<string>();

            _client = client;
        }

        // noget som Flask har, en dictionary, som returner det relevante API url som skal kaldes  
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

        public async Task GetUserID(string username)
        {
            var response = await _client.GetAsync($"users/{username}");
        }
    }

}