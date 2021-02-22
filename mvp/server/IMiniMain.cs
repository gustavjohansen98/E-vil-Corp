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

        Task<IEnumerable<UserMessageDTO>> UserTimeline(int u_id);

        void AddUserToDB(string username, string email, string password);

        void AddMessageToDB(string text);
    }
}