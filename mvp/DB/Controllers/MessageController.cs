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
        public ActionResult<IEnumerable<UserMessageDTO>> GetAllMessages([FromQuery(Name = "latest")] int latest)
        {
            LatestController.UpdateLATEST(latest);
            // TODO: not_req-from_simulator
            
            var messages = _repoMessage.GetAllMessages();

            return Ok(messages);            
        }

        [HttpGet("{username}")]
        public ActionResult<IEnumerable<UserMessageDTO>> GetMessagesFromAGivenUser(string username, [FromQuery(Name = "latest")] int latest)
        {
            LatestController.UpdateLATEST(latest);

            // TODO: not re_from_reposonse

            var user_id = _repoUser.GetUserIDFromUsername(username);
            
            return Ok(_repoMessage.GetAllMessageFromUser(user_id));
        }

        [HttpPost("{username}")]
        public IActionResult Tweet(string username, [FromBody] JsonElement body, [FromQuery(Name = "latest")] int latest)
        {
            LatestController.UpdateLATEST(latest);

            dynamic o = JsonConvert.DeserializeObject(body.ToString());
            string message = (string) o.content;

            var user_id = _repoUser.GetUserIDFromUsername(username);

            DateTime pub_date = DateTime.Now;

            _repoMessage.AddMessage(user_id, message, pub_date, 0);

            return Ok(null);    // with null as the argument, the action will result in a 204 status code rather than 200 ... 
        }

        [Route("{username}/follows")]
        [HttpGet]
        public ActionResult<IEnumerable<UserMessageDTO>> GetOwnAndFollowedMessages(string username, [FromQuery(Name = "latest")] int latest)
        {
            LatestController.UpdateLATEST(latest);

            var user_id = _repoUser.GetUserIDFromUsername(username);

            var messages = _repoMessage.GetOwnAndFollowedMessages(user_id);

            return Ok(messages);
        }
    }   
}