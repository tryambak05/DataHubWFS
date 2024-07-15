using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Wfs.Business.Models.HttpClientServiceModel;
using Wfs.Model.Helper;

namespace Wfs.Business.HttpClientServices
{
    public interface IWorldGraphServiceClient
    {
        Task<UploadAvailabilityDataReponse> PostWinUploadAvailabilityData(AvailabilityDataCommandModel availabilityDataCommandModel);
    }

    public class WorldGraphServiceClient : IWorldGraphServiceClient
    {
        public WorldGraphServiceClient(HttpClient httpClient, IApplicationConfiguration applicationConfiguration, IAuthServiceClient authServiceClient)
        {
            this._HttpClient = httpClient;
            this._ApplicationConfiguration = applicationConfiguration;
            this._AuthServiceClient = authServiceClient;
            SetUpHttpRequestParameters();
        }

        private HttpClient _HttpClient { get; set; }
        private IApplicationConfiguration _ApplicationConfiguration { get; set; }

        private IAuthServiceClient _AuthServiceClient { get; set; }

        public async Task<UploadAvailabilityDataReponse> PostWinUploadAvailabilityData(AvailabilityDataCommandModel availabilityDataCommandModel)
        {
            var json = JsonConvert.SerializeObject(availabilityDataCommandModel);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await this._HttpClient
                    .PostAsync(_ApplicationConfiguration.ApplicationService.DataHubServiceUrl, httpContent);
                if (response.IsSuccessStatusCode == true)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    if(string.IsNullOrEmpty(jsonString) == false)
                    {
                        var result = JsonConvert.DeserializeObject<UploadAvailabilityDataReponse>(jsonString);
                        return result;
                    }
                }
            }
            catch (HttpRequestException exception)
            {
                // TODO Excetion Logging
                throw new Exception(exception.Message);
            }

            // TODO Logging
            return null;
        }

        private void SetUpHttpRequestParameters()
        {
            this._HttpClient.DefaultRequestHeaders.Accept.Clear();
            this._HttpClient.DefaultRequestHeaders.Add("accept", "application/json");

            var authToken = _AuthServiceClient.AuthToken().Result;

            this._HttpClient.BaseAddress = new Uri(this._ApplicationConfiguration.ApplicationService.DataHubBaseUrl);
            // TOKEN PASS THROUGH authToken.token
            this._HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");
        }
    }
}