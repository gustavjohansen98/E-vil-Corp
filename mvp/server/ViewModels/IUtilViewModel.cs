using System.Collections.Generic;
using Minitwit.Entities;

namespace mvp.ViewModels
{
    public interface IUtilViewModel
    {
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
    }
}