using Repos;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minitwit.Entities;
using System.Linq;

namespace BlazorServer
{
    [Route("/msgs/")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageRepository _repoMessage;
        private IUserRepository _repoUser;
        private const int LIMIT = 100;

        public MessageController(IMessageRepository repoMessage, IUserRepository repoUser)
        {
            _repoMessage = repoMessage;
            _repoUser = repoUser;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Message>> GetAllMessages()
        {
            // TODO: not_req-from_simulator
            
            var users = _repoUser.GetAllUsers();
            
            var messages = (from m in _repoMessage.GetAllMessages().ToList()
                           where m.flagged == 0
                           orderby m.pub_date
                           // Filtered messages
                           select new 
                           { 
                               content = m.text,
                               pub_date = m.pub_date,
                               user = users.Where(u => u.ID == m.author_ID).Select(u => u.username)
                           }).Take(LIMIT);
            
            return Ok(messages);            
        }

        [HttpGet("{username}")]
        public ActionResult<IEnumerable<Message>> GetMessagesFromAGivenUser(string username)
        {
            // TODO update latest

            // TODO: not re_from_reposonse

            var user_id = _repoUser.GetUserIDFromUsername(username);

            if (user_id == -1) return NotFound();

            var messages = (from m in _repoMessage.GetAllMessages()
                           where m.flagged == 0 &&
                                 m.author_ID == user_id // user_id?
                           orderby m.pub_date
                           select new 
                           {
                               content = m.text,
                               pub_date = m.pub_date,
                               user = username
                           }).Take(LIMIT);

            return Ok(messages);
            throw new NotImplementedException();
        }

        [HttpPost("{username}")]
        public IActionResult CreateMessage([FromBody] Message message)
        {
            _repoMessage.AddMessage(message);

            return Ok();
        }
    }   
}