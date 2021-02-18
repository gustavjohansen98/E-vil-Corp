using Repos;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minitwit.Entities;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;

namespace Controllers
{
    [Route("/msgs/")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageRepository _repoMessage;
        private IUserRepository _repoUser;
        private const int LIMIT = 100; // (y) noice 

        public MessageController(IMessageRepository repoMessage, IUserRepository repoUser)
        {
            _repoMessage = repoMessage;
            _repoUser = repoUser;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Message>> GetAllMessages([FromQuery(Name = "latest")] int latest)
        {
            LatestController.UpdateLATEST(latest);
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
                               user = users.Where(u => u.user_id == m.author_id).Select(u => u.username)
                           }).Take(LIMIT);
            
            return Ok(messages);            
        }

        [HttpGet("{username}")]
        public ActionResult<IEnumerable<Message>> GetMessagesFromAGivenUser(string username, [FromQuery(Name = "latest")] int latest)
        {
            LatestController.UpdateLATEST(latest);

            // TODO: not re_from_reposonse

            var user_id = _repoUser.GetUserIDFromUsername(username);

            if (user_id != -1) return NotFound();

            var messages = (from m in _repoMessage.GetAllMessages()
                           where m.flagged == 0 &&
                                 m.author_id == user_id 
                           orderby m.pub_date
                           select new 
                           {
                               content = m.text,
                               pub_date = m.pub_date,
                               user = username
                           }).Take(LIMIT);

            return Ok(messages);
        }

        [HttpPost("{username}")]
        public IActionResult Tweet(string username, [FromBody] JsonElement body, [FromQuery(Name = "latest")] int latest)
        {
            LatestController.UpdateLATEST(latest);

            dynamic o = JsonConvert.DeserializeObject(body.ToString());

            string message = (string) o.content;

            var user_id = _repoUser.GetUserIDFromUsername(username);

            string pub_date = DateTime.Now.ToString();

            _repoMessage.AddMessage(user_id, message, pub_date, 0);

            return Ok(null);    // with null as the argument, the action will result in a 204 status code rather than 200 ... 
        }
    }   
}