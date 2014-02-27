using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pulsar
{
    class Creds
    {
        public static void writeConfig(string username, string apikey)
        {
            // open file
            // write config
            // close file
        }

        public static Tuple<string, string> readConfig()
        {
            return Tuple.Create("", "");
        }
    }
}
