using System;
using System.ComponentModel.DataAnnotations;

namespace Minitwit.Entities
{
    public class Message
    {
        public int ID { get; set; }

        [Required]
        public int author_ID { get; set; }

        [Required]
        public string text { get; set; } 

        public DateTime pub_date { get; set; }

        public int flagged { get; set; }

    }
}