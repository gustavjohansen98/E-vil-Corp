using EvilAPI.Repos;
using System;
using Microsoft.AspNetCore.Mvc;
using static System.Net.HttpStatusCode;
using System.Text.Json;
using Newtonsoft.Json;
using Prometheus;

namespace EvilAPI.Controllers
{
    [ApiController]
    [Route("/latest")]
    public class LatestController : ControllerBase
    {
        public static int LATEST { get; set ; }
        private static latest_global latest_;
        //private static Counter TikTok = Metrics.CreateCounter("sampleapp_ticks_total", "Just keeps on ticking");
        private static readonly Histogram LoginDuration = Metrics
    .CreateHistogram("myapp_login_duration_seconds", "Histogram of login call processing durations.");

        public LatestController(latest_global latest)
        {
            latest_ = latest;
        }

        [HttpGet]
        public IActionResult getLatest()
        {
            using (LoginDuration.NewTimer())
            {
                var latest = new { latest = latest_.LATEST };
                string output = JsonConvert.SerializeObject(latest);

                return Ok(output);
            }
            //TikTok.Inc();
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