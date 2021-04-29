using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Minitwit.Entities;
using Prometheus;
using static System.Net.HttpStatusCode;

namespace EvilAPI.Repos
{
    public class MessageRepository : IMessageRepository
    {
        private IMinitwitContext _context;
        private int LIMIT = 30;
        private static readonly Counter TickTock = Metrics.CreateCounter("sampleapp_ticks_total", "Just keeps on ticking");

        public MessageRepository(IMinitwitContext context)
        {
            _context = context;
        }

        public HttpStatusCode AddMessage(int author_id, string text, string pub_date, int flagged)
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

            var found = _context.Message.Where(m => m.author_id == author_id && m.text == text && m.pub_date == message.pub_date);

            if (found == null)
                return BadRequest;

            return NoContent;

        }

        public void AddMessage(Message message)
        {
            _context.Message.Add(message);
            _context.SaveChanges();
        }

        public IEnumerable<UserMessageDTO> GetAllMessageFromUser(int user_id)
        {
            return (from m in _context.Message
                    from u in _context.User
                    where m.flagged == 0 &&
                    m.author_id == u.user_id &&
                    u.user_id == user_id
                    orderby m.message_id descending
                    select new UserMessageDTO
                    {
                        username = u.username,
                        email = u.email,
                        text = m.text,
                        pub_date = m.pub_date
                    }).Take(LIMIT);
        }

        public IEnumerable<UserMessageDTO> GetAllMessages()
        {

            // 2021-04-15 14:20:47.5910938

            TickTock.Inc();
            return (from m in _context.Message
                    from u in _context.User
                    where m.flagged == 0 &&
                    m.author_id == u.user_id
                    orderby m.message_id descending
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
                   orderby m.message_id descending
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
