using System;
using System.Collections.Generic;
using Minitwit.Entities;

namespace Repos
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetAllMessages();

        void AddMessage(int author_id, string text, DateTime pub_date, int flagged);

        void AddMessage(Message message);

        IEnumerable<Message> GetAllMessageFromUser(int user_id);
    }
}