using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Disty.Common.Contract;
using Newtonsoft.Json;

namespace Disty.Service.Client
{
    public interface IDistyClient<T> where T : DistyEntity
    {
        Task<T> FindAsync(string path);
        
        Task<IEnumerable<T>> GetAsync(string path);
    }

    public class DistyClient<T> : HttpClient, IDistyClient<T> where T : DistyEntity
    {
        public DistyClient()
            : base(new HttpClientHandler() { PreAuthenticate = true, UseDefaultCredentials = true })
        {
            var baseUri = ConfigurationManager.AppSettings["baseUri"] as string;
            if (string.IsNullOrEmpty(baseUri))
                throw new InvalidOperationException(string.Format("Cannot determine baseUri from AppSettings in the configuration file.  Looking in:  {0}", AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));

            BaseAddress = new Uri(baseUri);
        }

        public async Task<T> FindAsync(string path)
        {
            return await base.GetAsync(string.Concat(BaseAddress, "/", path)).ContinueWith<T>(t =>
            {
                var response = t.Result;
                if(response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsAsync<T>();
                    if (content.Status != TaskStatus.Faulted)
                        return content.Result;
                    else
                        throw content.Exception;
                }

                return default(T) as T;
            });
        }

        public async Task<IEnumerable<T>> GetAsync(string path)
        {
            return await base.GetAsync(string.Concat(BaseAddress, "/", path)).ContinueWith<IEnumerable<T>>(t =>
            {
                var response = t.Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsAsync<List<T>>();
                    if (content.Status != TaskStatus.Faulted)
                        return content.Result;
                    else
                        throw content.Exception;
                }

                return new List<T>() as IEnumerable<T>;
            });
        }

    }
}
