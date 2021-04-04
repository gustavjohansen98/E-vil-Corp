// using System.ComponentModel.DataAnnotations;

namespace Minitwit.Entities
{
    public class Follower
    {
        // [Key]
        public int who_id { get; set; }

        // [Key]
        public int whom_id { get; set; }
    }
}