using System;
using System.ComponentModel.DataAnnotations;

namespace Minitwit.Entities
{
    public class Message
    {
        [Key]
        public int message_id { get; set; }

        [Required]
        public int author_id { get; set; }

        [Required]
        public string text { get; set; } 

        public string pub_date { get; set; }

        public int flagged { get; set; }

    }
}