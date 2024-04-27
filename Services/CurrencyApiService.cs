using Newtonsoft.Json;
using WalletApi.Models.DTOs;
using WalletApi.Services.Interfaces;

namespace WalletApi.Services
{
    public class CurrencyApiService(IConfiguration configuration, HttpClient httpClient) : ICurrencyApiService
    {
        private readonly string _baseUrl = configuration.GetSection("CurrencyApiSettings:BaseUrl").Value;
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _config = configuration;

        public async Task<ApiResponse<T>> MakeRequestAsync<T>(ApiRequest request)
        {

            if (request.QueryParams.Any())
            {
                foreach (var kvp in request.QueryParams)
                {
                    _httpClient.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                }
            }

            HttpResponseMessage? response = null;
            switch (request.ApiType)
            {
                case "GET":
                    response = await _httpClient.GetAsync(new Uri($"{_baseUrl}{request.Endpoint}"));
                    break;

                default:
                    return new ApiResponse<T>
                    {
                        IsSuccess = false,
                        Error = "Invalid request type"
                    };
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(content);
                return new ApiResponse<T>
                {
                    IsSuccess = true,
                    Data = result
                };
            }

            return new ApiResponse<T>
            {
                IsSuccess = false,
                Error = response.ReasonPhrase
            };
        }

 
    }
}
