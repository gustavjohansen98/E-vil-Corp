using System;
using System.Collections.Generic;
using System.Linq;
using Minitwit.Entities;

namespace Repos
{
    public class MessageRepository : IMessageRepository
    {
        private IMinitwitContext _context;

        public MessageRepository(IMinitwitContext context)
        {
            _context = context;
        }

        public void AddMessage(int author_id, string text, DateTime pub_date, int flagged)
        {
            var message = new Message
            { 
                author_ID = author_id,
                text = text,
                pub_date = pub_date,
                flagged = flagged
            };

            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public IEnumerable<Message> GetAllMessageFromUser(int user_id)
        {
            return _context.Messages.Where(m => m.flagged == 0 && m.author_ID == user_id);
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _context.Messages;
        }
    }
}