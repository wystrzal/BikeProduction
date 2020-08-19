using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BikeHttpClient
{
    public interface ICustomHttpClient
    {
        Task<string> GetStringAsync(string uri, string authorizationToken = null, Dictionary<string, string> queryParams = null);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null);
        Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null);
        Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null);
    }
}
