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
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private IUserRepository _repoUser;

        public UserController(IUserRepository repoUser)
        {
            _repoUser = repoUser;
        }

        [HttpGet("{username}")]
        public ActionResult<User> GetUser(string username)
        {
            var userID = _repoUser.GetUserIDFromUsername(username);

            var user = _repoUser.GetUserFromID(userID);

            return Ok(user);
        }
    }
}