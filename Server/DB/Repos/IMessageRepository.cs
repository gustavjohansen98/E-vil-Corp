using System;
using System.Collections.Generic;
using Minitwit.Entities;

namespace Repos
{
    public interface IMessageRepository
    {
        void AddMessage(int author_id, string text, DateTime pub_date, int flagged);

        IEnumerable<Message> GetAll();
    }
}