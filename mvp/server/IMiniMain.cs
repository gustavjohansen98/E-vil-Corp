using System.Collections;
using System.Collections.Generic;
using Minitwit.Entities;

namespace mvp
{
    public interface IMiniMain
    {
        User User { get; set; }
        IEnumerable<string> FlashedMessages { get; set; }
        IEnumerable<UserMessageDTO> UserMessageDTO { get; set; }
        string URL { get; }

        string Url_for(string name);

        string UrlForUser(string username);

        string UrlForUnfollow(string username);

        string UrlForFollow(string username);

        string GravatarUrl(string email, int size=80);

        IEnumerable<UserMessageDTO> Timeline();

        IEnumerable<UserMessageDTO> PublicTimeline();

        IEnumerable<UserMessageDTO> UserTimeline(int u_id);

        void AddUserToDB(string username, string email, string password);

        void AddMessageToDB(string text);
    }
}