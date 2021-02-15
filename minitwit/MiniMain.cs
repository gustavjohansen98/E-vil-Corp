using System;

namespace minitwit 
{
    public class MiniMain : IMiniMain
    {
        public string User { get; set; }
        private string URL = "";

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