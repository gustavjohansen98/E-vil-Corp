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

        public IEnumerable<UserMessageDTO> Timeline();
    }
}