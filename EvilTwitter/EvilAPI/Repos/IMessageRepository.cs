using System;
using System.Collections.Generic;
using System.Net;
using Minitwit.Entities;

namespace EvilAPI.Repos
{
    public interface IMessageRepository
    {
        HttpStatusCode AddMessage(int author_id, string text, string pub_date, int flagged);

        void AddMessage(Message message);

        IEnumerable<UserMessageDTO> GetAllMessageFromUser(int user_id);

        IEnumerable<UserMessageDTO> GetAllMessages();

        IEnumerable<UserMessageDTO> GetOwnAndFollowedMessages(int user_id);
    }
}