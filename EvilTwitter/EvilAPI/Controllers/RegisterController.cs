using EvilAPI.Repos;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minitwit.Entities;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;

namespace EvilAPI.Controllers
{
    [ApiController]
    [Route("/register")]
    public class RegisterController : ControllerBase
    {
        private IUserRepository _repo;
        private static latest_global latest_;

        public RegisterController(IUserRepository repo, latest_global LATEST)
        {
            _repo = repo;
            latest_ = LATEST;
        }

        [HttpPost]
        public IActionResult CreateNewUser([FromBody]JsonElement body, [FromQuery(Name = "latest")] int latest)
        {
            latest_.LATEST = latest;           ;

            dynamic o = JsonConvert.DeserializeObject(body.ToString());

            string username = null;
            string email = null;
            string pwd = null;

            try {
                username = (string) o.username;
                email = (string) o.email;
                pwd = (string) o.pwd;
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest("invalid");
            }

            if (o == null)
                return BadRequest("no user object received");

            if (username == null)
                return BadRequest("You have to enter a username");
            
            if (_repo.GetUserIDFromUsername(username) != -1)
                return BadRequest("The username is already taken");


            if (email == null || !email.Contains("@")) 
                return BadRequest("You have to enter a valid email address");

            if (pwd == null)
                return BadRequest("You have to enter a password");

            var user = new User {
                username = username,
                email = email,
                pwd = pwd
            };
            
            var statusCode = _repo.AddUser(user);

            return new NoContentResult();
            
        }
    }
}