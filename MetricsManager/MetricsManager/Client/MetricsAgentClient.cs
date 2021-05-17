using MetricsManager.Responses;
using MetricsManager.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Newtonsoft.Json;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;

            _logger = logger;
        }
        
        public CpuMetricsApiResponse GetCpuMetrics(CpuMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post,
                $"{request.ClientBaseAddress}/api/metrics/agent/cpu/getbytimeperiod?");

            //Серилизовываем запрос
            var requestObject = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(requestObject);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            httpRequest.Content = byteContent;

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                response.EnsureSuccessStatusCode();

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                return System.Text.Json.JsonSerializer.DeserializeAsync<CpuMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public DotNetMetricsApiResponse GetDotNetMetrics(DotNetMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post,
                $"{request.ClientBaseAddress}/api/metrics/agent/dotnet/getbytimeperiod?");

            //Серилизовываем запрос
            var requestObject = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(requestObject);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            httpRequest.Content = byteContent;

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                response.EnsureSuccessStatusCode();

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                //Включаем caseInSensitive
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                return System.Text.Json.JsonSerializer.DeserializeAsync<DotNetMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public HddMetricsApiResponse GetHddMetrics(HddMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post,
                $"{request.ClientBaseAddress}/api/metrics/agent/hdd/getbytimeperiod?");

            //Серилизовываем запрос
            var requestObject = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(requestObject);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            httpRequest.Content = byteContent;

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                response.EnsureSuccessStatusCode();

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                return System.Text.Json.JsonSerializer.DeserializeAsync<HddMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public NetworkMetricsApiResponse GetNetworkMetrics(NetworkMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post,
                $"{request.ClientBaseAddress}/api/metrics/agent/network/getbytimeperiod?");

            //Серилизовываем запрос
            var requestObject = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(requestObject);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            httpRequest.Content = byteContent;

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                response.EnsureSuccessStatusCode();

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                return System.Text.Json.JsonSerializer.DeserializeAsync<NetworkMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public RamMetricsApiResponse GetRamMetrics(RamMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post,
                $"{request.ClientBaseAddress}/api/metrics/agent/ram/getbytimeperiod?");

            //Серилизовываем запрос
            var requestObject = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(requestObject);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            httpRequest.Content = byteContent;

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                response.EnsureSuccessStatusCode();

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                return System.Text.Json.JsonSerializer.DeserializeAsync<RamMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}