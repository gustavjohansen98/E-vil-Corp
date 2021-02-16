using Repos;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minitwit.Entities;
using System.Linq;

namespace BlazorServer
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
        public IActionResult CreateNewUser([FromBody]User user)
        {

            Console.WriteLine("ok");

            if (user == null)
                return BadRequest("no user object received");

            if (user.username == null)
                return BadRequest("You have to enter a username");
            
            if (_repo.GetUserIDFromUsername(user.username) != -1)
                return BadRequest("The username is already taken");

            if (user.email == null || !user.email.Contains("@")) 
                return BadRequest("You have to enter a valid email address");

            if (user.pw_hash == null)
                return BadRequest("You have to enter a password");

            
            var statusCode = _repo.AddUser(user);

            return new NoContentResult();
            
        }
    }
}