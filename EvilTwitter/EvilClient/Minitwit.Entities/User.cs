// using System.ComponentModel.DataAnnotations;

namespace Minitwit.Entities
{
    public class User
    {
        // [Key]
        public int user_id { get; set; }

        // [Required]
        public string username { get; set; }

        // [Required]
        public string email { get; set; } 

        // [Required]
        public string pwd { get; set; }
    }
}