using Microsoft.EntityFrameworkCore;

namespace Minitwit.Entities
{

    public interface IMinitwitContext
    {
        DbSet<User> Users { get; } 
        DbSet<Follower> Followers { get; }
        DbSet<Message> Messages { get; }
        int SaveChanges();
    }
}