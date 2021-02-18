using System;
using System.Collections.Generic;
using Minitwit.Entities;

namespace Repos
{
    public interface IMessageRepository
    {
        void AddMessage(int author_id, string text, string pub_date, int flagged);

        void AddMessage(Message message);

        IEnumerable<UserMessageDTO> GetAllMessageFromUser(int user_id);

        public IEnumerable<UserMessageDTO> GetAllMessages();

        public IEnumerable<UserMessageDTO> GetOwnAndFollowedMessages(int user_id);
    }
}