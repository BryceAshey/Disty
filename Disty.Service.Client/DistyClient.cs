using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Disty.Service.Client
{
    public interface IDistyClient
    {
         Task<HttpResponseMessage> GetAsync(string path);
    }

    public class DistyClient : HttpClient, IDistyClient
    {
        public DistyClient()
        {
            var baseUri = ConfigurationManager.AppSettings["baseUri"] as string;
            if (string.IsNullOrEmpty(baseUri))
                throw new InvalidOperationException(string.Format("Cannot determine baseUri from AppSettings in the configuration file.  Looking in:  {0}", AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));

            BaseAddress = new Uri(baseUri);
        }


        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            Uri uri;
            if (!Uri.TryCreate(BaseAddress, path, out uri))
                throw new InvalidOperationException(string.Format("Unable to create Uri from base {0} and relative {1}.", BaseAddress, path));

            return await base.GetAsync(uri);
        }

    }
}
