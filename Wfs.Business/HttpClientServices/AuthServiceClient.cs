using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wfs.Business.Models.HttpClientServiceModel;
using Wfs.Model.Helper;

namespace Wfs.Business.HttpClientServices
{
    public interface IAuthServiceClient
    {
        Task<AuthResponse> AuthToken();
    }

    /// <summary>
    /// Auth Service Client
    /// </summary>
    public class AuthServiceClient : IAuthServiceClient
    {
        private readonly HttpClient _HttpClient;
        public AuthServiceClient(HttpClient httpClient, IApplicationConfiguration applicationConfiguration)
        {
            this._HttpClient = httpClient;
            this._ApplicationConfiguration = applicationConfiguration;
        }

        private IApplicationConfiguration _ApplicationConfiguration { get; set; }

        public async Task<AuthResponse> AuthToken()
        {
            var authRequest = new AuthRequest()
            {
                client_id = this._ApplicationConfiguration.OAuthCredentails.client_id,
                client_secret = this._ApplicationConfiguration.OAuthCredentails.client_secret,
                audience = this._ApplicationConfiguration.OAuthCredentails.audience,
                grant_type = this._ApplicationConfiguration.OAuthCredentails.grant_type
            };

            var json = JsonConvert.SerializeObject(authRequest);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await this._HttpClient.PostAsync("url", httpContent);
                if (response.IsSuccessStatusCode == true)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    if(string.IsNullOrEmpty(jsonString) == false)
                    {
                        var result = JsonConvert.DeserializeObject<AuthResponse>(jsonString);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO Excetion Logging

            }

            // TODO Logging
            return null;
        }
    }
}
