using System.Collections;
using System.Collections.Generic;
using Minitwit.Entities;

namespace mvp
{
    public interface IMiniMain
    {
        User User { get; set; }
        IEnumerable<UserMessageDTO> UserMessageDTO { get; set; }
        string URL { get; }

        string Url_for(string name);

        string UrlForUnfollow(string username);

        string UrlForFollow(string username);

        public string GravatarUrl(string email, int size=80);

        public IEnumerable<UserMessageDTO> Timeline();

        public IEnumerable<UserMessageDTO> PublicTimeline();

        public IEnumerable<UserMessageDTO> UserTimeline();
    }
}