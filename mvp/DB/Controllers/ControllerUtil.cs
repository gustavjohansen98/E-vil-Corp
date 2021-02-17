using Repos;
using System;
using Microsoft.AspNetCore.Mvc;

using static System.Net.HttpStatusCode;

using System.Text.Json;
using Newtonsoft.Json;

namespace Controllers
{
    public class ControllerUtil : ControllerBase
    {
        public int LATEST { get; }

        public ControllerUtil()
        {
            LATEST = 0;
        }

        public void Not_req_from_simulator()
        {
            throw new NotImplementedException();
        }

        public void UpdateLATEST()
        {
            
        }
    }
}