using Repos;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minitwit.Entities;
using System.Linq;

namespace Controllers
{
    [ApiController]
    [Route("/register")]
    public class RegisterController : ControllerBase
    {
        private IUserRepository _repo;

        public RegisterController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult CreateNewUser([FromBody]User user, [FromQuery(Name = "latest")] int latest)
        {
            LatestController.UpdateLATEST(latest);

            if (user == null)
                return BadRequest("no user object received");

            if (user.username == null)
                return BadRequest("You have to enter a username");
            
            if (_repo.GetUserIDFromUsername(user.username) != -1)
                return BadRequest("The username is already taken");

            if (user.email == null || !user.email.Contains("@")) 
                return BadRequest("You have to enter a valid email address");

            if (user.pwd == null)
                return BadRequest("You have to enter a password");

            
            var statusCode = _repo.AddUser(user);

            return new NoContentResult();
            
        }
    }
}