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
        public static string path = Environment.ExpandEnvironmentVariables("%APPDATA%\\pulsar.json");
        
        public static void writeConfig(string Username, string Apikey)
        {
            Credentials c = new Credentials 
            {
                username = Username,
                apikey = Apikey
            };
            File.WriteAllText(path, JsonConvert.SerializeObject(c));
        }

        public static Credentials readConfig()
        {
            var file = File.ReadAllText(path);
            Credentials creds = JsonConvert.DeserializeObject<Credentials>(file);
            return creds;
        }
    }

    class Credentials
    {
        public string username { get; set; }
        public string apikey { get; set; }
    }
}
