using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Minitwit.Entities;

namespace mvp
{
    public interface IMiniMain
    {
        User User { get; set; }
        IEnumerable<string> FlashedMessages { get; set; }
        IEnumerable<UserMessageDTO> UserMessageDTO { get; set; }
        string URL { get; }
        string APIURL { get; }

        string MD5Hasher(string toBeHashed);

        string Url_for(string name);

        string UrlForUser(string username);

        string UrlForUnfollow(string username);

        string UrlForFollow(string username);

        string GravatarUrl(string email, int size=80);

        Task<IEnumerable<UserMessageDTO>> Timeline();

        Task<IEnumerable<UserMessageDTO>> PublicTimeline();

        Task<IEnumerable<UserMessageDTO>> UserTimeline(string username);

        Task AddUserToDB(string username, string email, string password);

        Task AddMessageToDB(string text);

        Task Login(string username);

        Task<User> GetUserFromUsername(string username);

        Task<bool> IsFollowed(string username1, string username2);

        Task FollowUser(string user, string userToUnfollow);

        Task UnfollowUser(string user, string userToFollow);
    }
}