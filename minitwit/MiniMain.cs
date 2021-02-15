using System;
using System.Collections;
using System.Collections.Generic;

namespace minitwit 
{
    public class MiniMain : IMiniMain
    {
        public string User { get; set; }
        public IEnumerable<string> Messages { get; set; }
        private string URL = "";

        public MiniMain()
        {
            Messages = new List<string>();
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

            throw new NotImplementedException();
        }
    }

}