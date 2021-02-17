using System;
using System.Net.Http;

namespace Controllers
{
    public class ControllerUtil
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