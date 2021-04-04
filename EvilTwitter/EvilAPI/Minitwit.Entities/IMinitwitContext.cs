using Microsoft.EntityFrameworkCore;

namespace Minitwit.Entities
{

    public interface IMinitwitContext
    {
        DbSet<User> User { get; } 
        DbSet<Follower> Follower { get; }
        DbSet<Message> Message { get; }
        int SaveChanges();
    }
}