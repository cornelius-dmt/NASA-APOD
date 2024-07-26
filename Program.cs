using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NasaApodIntegration;
using NasaApodIntegration.Services;

// See https://aka.ms/new-console-template for more information
class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();
        var apodService = serviceProvider.GetRequiredService<ApodService>();
        var configService = serviceProvider.GetRequiredService<ConfigurationService>();

        try
        {
            await apodService.ProcessApodAsync();
            Console.WriteLine("APOD processing completed successfully.");

            var settings = configService.GetAppSettings();
            ValidateResults(settings.Date);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void ValidateResults(string date)
    {
        string imageFile = $"APOD_{date}.jpg";
        string descFile = $"APOD_{date}_description.txt";

        if (File.Exists(imageFile))
        {
            Console.WriteLine($"Image file created: {imageFile}");
            Console.WriteLine($"Image file size: {new FileInfo(imageFile).Length} bytes");
        }
        else
        {
            Console.WriteLine($"Error: Image file not found: {imageFile}");
        }

        if (File.Exists(descFile))
        {
            Console.WriteLine($"Description file created: {descFile}");
            string content = File.ReadAllText(descFile);
            Console.WriteLine($"Description length: {content.Length} characters");
        }
        else
        {
            Console.WriteLine($"Error: Description file not found: {descFile}");
        }
    }

    private static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<ConfigurationService>()
            .AddSingleton<FileService>()
            .AddSingleton<ApodService>()
            .BuildServiceProvider();
    }
}
