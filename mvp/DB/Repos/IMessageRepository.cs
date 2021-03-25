using System;
using System.Collections.Generic;
using Minitwit.Entities;

namespace Repos
{
    public interface IMessageRepository
    {
        void AddMessage(int author_id, string text, DateTime pub_date, int flagged, string flagged2 = "");

        void AddMessage(Message message);

        IEnumerable<UserMessageDTO> GetAllMessageFromUser(int user_id);

        IEnumerable<UserMessageDTO> GetAllMessages();

        IEnumerable<UserMessageDTO> GetOwnAndFollowedMessages(int user_id);
    }
}