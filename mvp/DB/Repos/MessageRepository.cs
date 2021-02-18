using System;
using System.Collections.Generic;
using System.Linq;
using Minitwit.Entities;

namespace Repos
{
    public class MessageRepository : IMessageRepository
    {
        private IMinitwitContext _context;
        private int LIMIT = 30;

        public MessageRepository(IMinitwitContext context)
        {
            _context = context;
        }

        public void AddMessage(int author_id, string text, string pub_date, int flagged)
        {
            var message = new Message
            { 
                author_id = author_id,
                text = text,
                pub_date = pub_date,
                flagged = flagged
            };

            _context.Message.Add(message);
            _context.SaveChanges();
        }

        public void AddMessage(Message message)
        {
            _context.Message.Add(message);
            _context.SaveChanges();
        }

        public IEnumerable<UserMessageDTO> GetAllMessageFromUser(int user_id)
        {
            // return _context.Message.Where(m => m.flagged == 0 && m.author_id == user_id);
            throw new NotImplementedException();
        }

        public IEnumerable<UserMessageDTO> GetAllMessages()
        {
            return (from m in _context.Message
                    from u in _context.User
                    where m.flagged == 0 &&
                    m.author_id == u.user_id
                    orderby m.pub_date
                    select new UserMessageDTO
                    {
                            username = u.username,
                            email = u.email,
                            text = m.text,
                            pub_date = m.pub_date
                    }).Take(LIMIT);
        }

        public IEnumerable<UserMessageDTO> GetOwnAndFollowedMessages(int user_id)
        {
            return (from m in _context.Message
                   from u in _context.User
                   where m.flagged == 0 &&
                   m.author_id == u.user_id &&
                   (
                       u.user_id == user_id ||
                       (from f in _context.Follower
                        where f.who_id == user_id
                        select f.whom_id).Contains(u.user_id)
                   )
                   orderby m.pub_date
                   select new UserMessageDTO
                   {
                        username = u.username,
                        email = u.email,
                        text = m.text,
                        pub_date = m.pub_date
                   }).Take(LIMIT);
        }

        
    }
}