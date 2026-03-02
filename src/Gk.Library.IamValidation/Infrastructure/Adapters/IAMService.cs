using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GK.Library.IamValidation.Application.Ports;
using GK.Library.IamValidation.Infrastructure.Models;

namespace GK.Library.IamValidation.Infrastructure.Adapters
{
    public class IamService : IIamService
    {
        private static readonly JsonSerializerOptions _jsonOpts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        private readonly IamServiceSetting _iAMServiceSetting;
        private readonly HttpClient _httpClient;

        public IamService(IamServiceSetting options)
        {
            _iAMServiceSetting = options;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_iAMServiceSetting.BaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(_iAMServiceSetting.TimeOutInSeconds);
        }

        public async Task<AuthValidateResponse> Validate(AuthValidateRequest request)
        {
            var body = JsonSerializer.Serialize(request, _jsonOpts);
            using var content = new StringContent(body, Encoding.UTF8, "application/json");

            var basic64 = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{request.Username}:{request.Password}"));
            using var httpReq = new HttpRequestMessage(HttpMethod.Post, _iAMServiceSetting.EndpointAuthValidate)
            {
                Content = content
            };
            httpReq.Headers.Authorization = new AuthenticationHeaderValue("Basic", basic64);
            httpReq.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var resp = await _httpClient.SendAsync(httpReq, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            
            using var stream = await resp.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var result = await JsonSerializer.DeserializeAsync<AuthValidateResponse>(stream, _jsonOpts).ConfigureAwait(false);

            return result;
        }
    }
}
