using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using net.openstack.Core.Exceptions;
using net.openstack.Core.Exceptions.Response;
using System.IO;

namespace pulsar
{
    class CloudFiles
    {
        private string container_name = "cybermallard";
        private CloudFilesProvider cf;

        public CloudFiles()
        {
            cf = this.authenticate();
        }

        private CloudFilesProvider authenticate()
        {
            Credentials creds = MainWindow.credentials;
            CloudFilesProvider cloudfiles;
            try
            {
                var raxcreds = new CloudIdentity
                {
                    Username = creds.username,
                    APIKey = creds.apikey
                };
                cloudfiles = new CloudFilesProvider(raxcreds);
                return cloudfiles;
            }
            catch (Exception ex)
            {
                MainWindow.ErrorMessage(ex.Message, "Error");
            }
            return null;
        }

        public Container get_container() 
        {

            try
            {
                this.cf.CreateContainer(container_name);
            }
            catch (Exception ex)
            {
                if (ex is UserNotAuthorizedException)
                {
                    MainWindow.ErrorMessage(ex.Message, "Error");
                    return null;
                }
                throw;
            }  // We don't care what the response is
            var containers = this.cf.ListContainers();
            foreach (Container c in containers)
            {
                if (c.Name == container_name)
                    return c;
            }
            return null;
        }

        private void get_container(string container_name) { }

        public bool upload_file(string container, string path)
        {
            try
            {
                this.cf.CreateObjectFromFile(container, Path.GetFullPath(path));
            }
            catch (Exception ex)
            {
                MainWindow.ErrorMessage(ex.Message, "Error");
                return false;
            }
            return true;   
        }
    }
}
