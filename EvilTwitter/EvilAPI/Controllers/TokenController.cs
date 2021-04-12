using Microsoft.AspNetCore.Mvc;
using System;

namespace EvilAPI.Controllers

{
    [ApiController]
    [Route("api/controller")]
    public class TokenController : ControllerBase
    {
        [HttpGet]
        public dynamic Get()
        {
            return new
            {
                Guid = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = Environment.MachineName
            };
        }
    }
}