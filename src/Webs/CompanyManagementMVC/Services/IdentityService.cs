using BikeHttpClient;
using CompanyManagementMVC.Interfaces;
using CompanyManagementMVC.Models;
using CompanyManagementMVC.Models.Dtos;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompanyManagementMVC.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly string baseUrl;
        private readonly ICustomHttpClient customHttpClient;
        private readonly ICookieAuthentication cookieAuthentication;

        public IdentityService(ICustomHttpClient customHttpClient, ICookieAuthentication cookieAuthentication)
        {
            baseUrl = "http://host.docker.internal:5000/api/identity/";
            this.customHttpClient = customHttpClient;
            this.cookieAuthentication = cookieAuthentication;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            string loginUrl = $"{baseUrl}login";

            var response = await customHttpClient.PostAsync(loginUrl, loginDto);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var tokenModel = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync());

                var tryLogin = await cookieAuthentication.SignIn(tokenModel);
                if (!tryLogin)
                    return false;           
            }
                
            return true;
        }
    }
}
