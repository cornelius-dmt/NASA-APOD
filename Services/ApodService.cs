using System;
using System.Net.Http;
using System.Threading.Tasks;
using NasaApodIntegration.Models;
using Newtonsoft.Json;

namespace NasaApodIntegration.Services
{

    public class ApodService
    {
        private readonly ConfigurationService _configService;
        private readonly FileService _fileService;
        private readonly HttpClient _httpClient;

        public ApodService(ConfigurationService configService, FileService fileService)
        {
            _configService = configService;
            _fileService = fileService;
            _httpClient = new HttpClient();
        }

        public async Task ProcessApodAsync()
        {
            var settings = _configService.GetAppSettings();
            var apiUrl = $"{settings.BaseUrl}?api_key={settings.ApiKey}&date={settings.Date}";

            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var apodResponse = JsonConvert.DeserializeObject<ApodResponse>(content);

            Console.WriteLine($"Retrieved APOD for date: {apodResponse.Date}, Title: {apodResponse.Title}");

            await _fileService.SaveImageAsync(apodResponse.Url, settings.Date);

            await _fileService.SaveDescriptionAsync(apodResponse.Explanation, settings.Date);
        }
    }
}