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
        private IFollowerRepository _repo;
        private IUserRepository _userRepo;

        public FollowerController(IFollowerRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
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
                var status = _repo.FollowUser(userToFollow, username);
                if (status == NotAcceptable)
                {
                    return BadRequest("could not follow user");
                }

                return Ok(null);
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
                var status = _repo.UnfollowUser(username, userToUnfollow);
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
    }
}