using System.IO;
using System;

namespace EvilAPI
{
    public static class GetConnectionString
    {
        public static String GetPsqlDbClusterConnectionString()
        {
            // var path = "../../../secrets.txt";
            var path = "/run/secrets/connectionstring";

            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    return sr.ReadLine();
                };
            }
            else
            {
                return null;
            }
        }
    }
}