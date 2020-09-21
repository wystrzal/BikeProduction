using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace BikeHttpClient
{
    public class CustomHttpClient : ICustomHttpClient
    {
        private readonly HttpClient client;

        public CustomHttpClient()
        {
            client = new HttpClient();
        }

        public async Task<string> GetStringAsync(string uri, string authorizationToken = null, Dictionary<string, string> queryParams = null)
        {
            if (queryParams != null)
            {
                uri = SetQueryParams(uri, queryParams);
            }

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            if (authorizationToken != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
            }

            var response = await client.SendAsync(requestMessage);

            return await response.Content.ReadAsStringAsync();
        }

        private string SetQueryParams(string uri, Dictionary<string, string> queryParams)
        {
            var builder = new UriBuilder(uri);

            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var queryParam in queryParams)
            {
                query[queryParam.Key] = queryParam.Value;
            };

            builder.Query = query.ToString();

            return builder.ToString();
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, string authorizationToken = null)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            var requestMessage = new HttpRequestMessage(method, uri);

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");

            if (authorizationToken != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
            }

            var response = await client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }


        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null)
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, item, authorizationToken);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null)
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, item, authorizationToken);
        }
        public async Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            if (authorizationToken != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
            }

            return await client.SendAsync(requestMessage);
        }
    }
}
