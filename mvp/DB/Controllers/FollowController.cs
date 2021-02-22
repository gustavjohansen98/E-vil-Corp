using Repos;
using System;
using Microsoft.AspNetCore.Mvc;

using static System.Net.HttpStatusCode;

using System.Text.Json;
using Newtonsoft.Json;


namespace Controllers
{
    [ApiController]
    [Route("/fllws")]
    public class FollowerController : ControllerBase
    {
        private IFollowerRepository _repoFollower;
        private IUserRepository _userRepo;

        public FollowerController(IFollowerRepository repoFollower, IUserRepository userRepo)
        {
            _repoFollower = repoFollower;
            _userRepo = userRepo;
        }

        [HttpPost("{username}")]
        public IActionResult Follow([FromRoute] string username, [FromBody] JsonElement body, [FromQuery(Name = "latest")] int latest)
        {
            LatestController.UpdateLATEST(latest);

            dynamic o = JsonConvert.DeserializeObject(body.ToString()); // deserialize to dynamic object, which we can add the relevant properties to

            string userToFollow = o.follow;   
            string userToUnfollow_unrefined = o.unfollow;
            string userToUnfollow = null;

            if (userToFollow != null)
            {
                var status = _repoFollower.FollowUser(userToFollow, username);
                if (status == NotAcceptable)
                {
                    return BadRequest("could not follow user");
                }

                return Ok(null);    // 204
            }

            // this is rather annoying : 
            //      - the unfollow content starts with an <'> everytime
            //      - perhaps due to json formatting error, but it needs to be trimmed to read properly from db
            //      - this i not a solution, since users should be able to have special chars in username 
            //              - or should they ?
            if (userToUnfollow_unrefined != null) 
            {
                userToUnfollow = userToUnfollow_unrefined.Replace("'", "");  
            }

            if (userToUnfollow != null)
            {
                var status = _repoFollower.UnfollowUser(username, userToUnfollow);
                if (status == NotAcceptable)
                {
                    return BadRequest("could not unfollow user");
                }

                return Ok(null);
            }

            return BadRequest("could not execute");            
        }

        [HttpGet]
        public IActionResult GetFollowers(string username)
        {
            throw new NotImplementedException();
        }

        [Route("{username1}/{username2}")]
        [HttpGet]
        public ActionResult<bool> DoesUser1FollowUser2(string username1, string username2)
        {
            var userID1 = _userRepo.GetUserIDFromUsername(username1);
            var userID2 = _userRepo.GetUserIDFromUsername(username2);

            var result = _repoFollower.DoesUserFollow(userID1, userID2);

            return Ok(result);
        }

        [Route("{username1}/{username2}")]
        [HttpPost]
        public IActionResult FollowUser(string username1, string username2)
        {
            _repoFollower.FollowUser(username1, username2);

            return Ok();
        }

        [Route("{username1}/{username2}")]
        [HttpDelete]
        public IActionResult UnfollowUser(string username1, string username2)
        {
            _repoFollower.UnfollowUser(username1, username2);

            return Ok();
        }
    }
}