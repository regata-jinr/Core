using Microsoft.Extensions.Configuration;

namespace Regata.Utilities
{
    public class ConfigManager
    {
        private IConfigurationRoot Configuration { get; set; }
        public string LogConnectionString;
        public string MainConnectionString;
        public string WebDavApiCredentials;

        public ConfigManager()
        {
            try
            {
                Configuration = new ConfigurationBuilder()
                          .AddUserSecrets<ConfigManager>()
                          .Build();

                LogConnectionString  = Configuration["LogConnectionString"];
                MainConnectionString = Configuration["MainConnectionString"];
                WebDavApiCredentials = Configuration["WebDavApiCredentials"];

            }
            catch
            {
                ;
            }
        }
    }
}
