using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace NasaApodIntegration.Services
{
    public class FileService
    {
        private readonly HttpClient _httpClient;

        public FileService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(5); // Increase timeout to 5 minutes
        }

        public async Task SaveImageAsync(string imageUrl, string date)
        {
            var fileName = $"APOD_{date}.jpg";
            try
            {
                Console.WriteLine($"Starting download of image from {imageUrl}");
                var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
                Console.WriteLine($"Download completed. Image size: {imageBytes.Length} bytes");

                await File.WriteAllBytesAsync(fileName, imageBytes);
                Console.WriteLine($"Image saved successfully as {fileName}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP error occurred while downloading the image: {e.Message}");
                throw;
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine($"The download operation timed out: {e.Message}");
                throw;
            }
            catch (IOException e)
            {
                Console.WriteLine($"An IO error occurred while saving the image: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
                throw;
            }
        }

        public async Task SaveDescriptionAsync(string description, string date)
        {
            var fileName = $"APOD_{date}_description.txt";
            await File.WriteAllTextAsync(fileName, description);
        }
    }
}