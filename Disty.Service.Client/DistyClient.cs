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
        {
            var baseUri = ConfigurationManager.AppSettings["baseUri"] as string;
            if (string.IsNullOrEmpty(baseUri))
                throw new InvalidOperationException(string.Format("Cannot determine baseUri from AppSettings in the configuration file.  Looking in:  {0}", AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));

            BaseAddress = new Uri(baseUri);
        }

        public async Task<T> FindAsync(string path)
        {
            Uri uri;
            if (!Uri.TryCreate(BaseAddress, path, out uri))
                throw new InvalidOperationException(string.Format("Unable to create Uri from base {0} and relative {1}.", BaseAddress, path));

            return await base.GetAsync(uri).ContinueWith<T>(task => {
                var response = task.Result;
                if(response.IsSuccessStatusCode)
                {
                    response.Content.ReadAsStreamAsync().ContinueWith(t =>
                    {
                        var stream = t.Result;
                        using (var sr = new StreamReader(stream))
                        {
                            using (var reader = new JsonTextReader(sr))
                            {
                                var serializer = new JsonSerializer();
                                return serializer.Deserialize<T>(reader);
                            }
                        }
                    });
                }

                return default(T);
            });
        }

        public async Task<IEnumerable<T>> GetAsync(string path)
        {
            Uri uri;
            if (!Uri.TryCreate(BaseAddress, path, out uri))
                throw new InvalidOperationException(string.Format("Unable to create Uri from base {0} and relative {1}.", BaseAddress, path));

            return await base.GetAsync(uri).ContinueWith<IEnumerable<T>>(task =>
            {
                var response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.ReadAsStreamAsync().ContinueWith(t =>
                    {
                        var stream = t.Result;
                        using (var sr = new StreamReader(stream))
                        {
                            using (var reader = new JsonTextReader(sr))
                            {
                                var serializer = new JsonSerializer();
                                return serializer.Deserialize<IEnumerable<T>>(reader);
                            }
                        }
                    });
                }

                return default(IEnumerable<T>);
            });
        }

    }
}
