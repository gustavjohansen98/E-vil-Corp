using Repos;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minitwit.Entities;
using System.Linq;
using System.Net;
using static System.Net.HttpStatusCode;
using System.IO;
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
        public IActionResult Follow([FromRoute] string username, [FromBody] JsonElement body)
        {
            dynamic o = JsonConvert.DeserializeObject(body.ToString()); // deserialize to dynamic object, which we can add the relevant properties to

            string userToFollow = o.follow;   
            string userToUnfollow = o.unfollow;

            if (userToFollow != null)
            {
                var status = _repo.FollowUser(username, userToFollow);
                if (status == NotAcceptable)
                    return BadRequest("could not follow user");

                return new NoContentResult();
            }

            if (userToUnfollow != null)
            {
                var status = _repo.UnfollowUser(username, userToUnfollow);
                if (status == NotAcceptable)
                    return BadRequest("could not unfollow user");

                return new NoContentResult();
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