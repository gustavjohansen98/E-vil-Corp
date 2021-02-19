using Repos;
using System;
using Microsoft.AspNetCore.Mvc;

using static System.Net.HttpStatusCode;

using System.Text.Json;
using Newtonsoft.Json;

namespace Controllers
{
    [ApiController]
    [Route("/latest")]
    public class LatestController : ControllerBase
    {
        public static int LATEST { get; set ; }

        public LatestController()
        {
            LATEST = 0;
        }

        [HttpGet]
        public IActionResult getLatest()
        {
            var latest = new { latest = LATEST };
            string output = JsonConvert.SerializeObject(latest);

            return Ok(output);
        }

        public static void Not_req_from_simulator()
        {
            throw new NotImplementedException();
        }

        // call this via a static context from the other Controllers 
        public static void UpdateLATEST(int latest)
        {
            try 
            {
                LATEST = latest;
            } catch (NullReferenceException e) 
            {
                Console.WriteLine(e.StackTrace);
                LATEST = -1;
            }
        }
    }
}