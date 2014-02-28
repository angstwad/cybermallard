using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using net.openstack.Core.Exceptions;

namespace pulsar
{
    class files
    {
        public static void authenticate()
        {
            try
            {
                var creds = new CloudIdentity
                    {
                        Username = "pulsar.user",
                        APIKey = "0715d203b2bc400db2b432b4da2308de"
                    };
                var identity = new CloudIdentityProvider(creds);
            }
            catch (UserAuthenticationException ex)
            {
                MainWindow.ErrorMessage(ex.Message, "Error");
            }

        }
    }
}
