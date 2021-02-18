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
            string userToUnfollow = o.unfollow;

            if (userToFollow != null)
            {
                var status = _repo.FollowUser(username, userToFollow);
                if (status == NotAcceptable)
                {
                    // Console.WriteLine($"could not follow user\nfollower : {userToFollow} \nusername {username}");
                    return BadRequest("could not follow user");
                }

                return new NoContentResult();
            }

            if (userToUnfollow != null)
            {
                var status = _repo.UnfollowUser(username, userToUnfollow);
                if (status == NotAcceptable)
                {
                    // Console.WriteLine("could not unfollow user");
                    return BadRequest("could not unfollow user");
                }

                return new NoContentResult();
            }

            // Console.WriteLine("could not exexute");
            return BadRequest("could not execute");            
        }


        [HttpGet]
        public IActionResult GetFollowers(string username)
        {
            throw new NotImplementedException();
        }
    }
}