using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace pulsar
{
    class CredsActions
    {
        public static void writeConfig(string Username, string Apikey)
        {
            Credentials c = new Credentials 
            {
                username = Username,
                apikey = Apikey
            };
            var path = Environment.ExpandEnvironmentVariables("%APPDATA%\\pulsar.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(c));
        }

        public static Credentials readConfig()
        {
            return new Credentials { }; // fix this later
        }
    }

    class Credentials
    {
        public string username { get; set; }
        public string apikey { get; set; }
    }
}
