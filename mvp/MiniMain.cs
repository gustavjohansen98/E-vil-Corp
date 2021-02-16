using System;
using System.Collections;
using System.Collections.Generic;

namespace mvp
{
    public class MiniMain : IMiniMain
    {
        public string User { get; set; }
        public IEnumerable<string> Messages { get; set; }

        public string URL { get; }


        public MiniMain()
        {
            URL = "http://localhost:5000/";
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

            return "";
        }
    }

}