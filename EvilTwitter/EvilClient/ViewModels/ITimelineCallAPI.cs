using System.Collections.Generic;
using System.Threading.Tasks;
using Minitwit.Entities;

namespace EvilClient.ViewModels
{
    public interface ITimelineCallAPI
    {
        Task<IEnumerable<UserMessageDTO>> Timeline();

        Task<IEnumerable<UserMessageDTO>> PublicTimeline();

        Task<IEnumerable<UserMessageDTO>> UserTimeline(string username);
    }
}
