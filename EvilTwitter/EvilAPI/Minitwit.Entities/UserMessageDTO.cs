using System;

namespace Minitwit.Entities
{
    public class UserMessageDTO
    {
        public string username { get; set; }
        public string email { get; set; }
        public string text { get; set; }
        public string pub_date { get; set; }

        public int flagged { get; set; }

        public int author_id { get; set; }
    }
}