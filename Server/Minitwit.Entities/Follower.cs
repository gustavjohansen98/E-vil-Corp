using System.ComponentModel.DataAnnotations;

namespace Minitwit.Entities
{
    public class Follower
    {
        public int who_id { get; set; }

        public int whom_id { get; set; }
    }
}