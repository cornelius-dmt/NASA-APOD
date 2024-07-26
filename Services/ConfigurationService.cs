using System.IO;
using NasaApodIntegration.Models;
using Newtonsoft.Json;

namespace NasaApodIntegration
{

    public class ConfigurationService
    {
        private const string ConfigFileName = "appsettings.json";
        private AppSettings _appSettings;

        public AppSettings GetAppSettings()
        {
            if (_appSettings == null)
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException($"Configuration file not found: {configPath}");
                }
                var json = File.ReadAllText(configPath);
                _appSettings = JsonConvert.DeserializeObject<AppSettings>(json);
            }

            return _appSettings;
        }
    }
}