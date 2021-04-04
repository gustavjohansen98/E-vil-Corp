using EvilAPI.Repos;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minitwit.Entities;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using static System.Net.HttpStatusCode;

namespace EvilAPI.Controllers
{
    [Route("/msgs/")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageRepository _repoMessage;
        private IUserRepository _repoUser;
        private const int LIMIT = 100; // (y) noice 
        private static latest_global latest_;

        public MessageController(IMessageRepository repoMessage, IUserRepository repoUser, latest_global LATEST)
        {
            _repoMessage = repoMessage;
            _repoUser = repoUser;
            latest_ = LATEST;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserMessageDTO>> GetAllMessages([FromQuery(Name = "latest")] int latest)
        {
            latest_.LATEST = latest;
            // TODO: not_req-from_simulator
            
            var messages = _repoMessage.GetAllMessages();

            return Ok(messages);            
        }

        [HttpGet("{username}")]
        public ActionResult<IEnumerable<UserMessageDTO>> GetMessagesFromAGivenUser(string username, [FromQuery(Name = "latest")] int latest)
        {
            latest_.LATEST = latest;

            // TODO: not re_from_reposonse

            var user_id = _repoUser.GetUserIDFromUsername(username);
            
            return Ok(_repoMessage.GetAllMessageFromUser(user_id));
        }

        [HttpPost("{username}")]
        public IActionResult Tweet(string username, [FromBody] JsonElement body, [FromQuery(Name = "latest")] int latest)
        {
            latest_.LATEST = latest;

            dynamic o = JsonConvert.DeserializeObject(body.ToString());
            string message = (string) o.content;

            var user_id = _repoUser.GetUserIDFromUsername(username);

            DateTime pub_date = DateTime.Now;

            var result = _repoMessage.AddMessage(user_id, message, pub_date, 0);

            if (result == System.Net.HttpStatusCode.BadRequest)
                return StatusCode(500);

            return Ok(null);    // with null as the argument, the action will result in a 204 status code rather than 200 ... 
        }

        [Route("{username}/follows")]
        [HttpGet]
        public ActionResult<IEnumerable<UserMessageDTO>> GetOwnAndFollowedMessages(string username, [FromQuery(Name = "latest")] int latest)
        {
            latest_.LATEST = latest;

            var user_id = _repoUser.GetUserIDFromUsername(username);

            var messages = _repoMessage.GetOwnAndFollowedMessages(user_id);

            return Ok(messages);
        }
    }   
}