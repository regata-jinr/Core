//using Microsoft.Extensions.Configuration;
//using System;

//namespace Regata.Utilities
//{
//    public class ConfigManager
//    {
//        private IConfigurationRoot Configuration { get; set; }
//        public string LogConnectionString;
//        public string GenConnectionStringBase;

//        private readonly IReadOnlyDictionary<string, string> _defaultSettings = new Dictionary<string, string>
//    {
//        {"Settings:SquirrelPath", @".nuget/packages/squirrel.windows/1.9.1/tools/Squirrel.exe"},
//        {"Settings:SquirrelArgs", "--no-msi --no-delta"},
//        {"Settings:DefaultReleasesPath", "Releases"},
//        {"Settings:Branch", "heads/master"},
//        {"Settings:PathToNupkg", @"bin\Release"}
//    };

//        public ConfigManager()
//        {
//            try
//            {
//                var builder = new ConfigurationBuilder();
//                builder.AddUserSecrets<ConfigManager>();

//                Configuration = new ConfigurationBuilder()
//                          .AddInMemoryCollection(_defaultSettings)
//                          .SetBasePath(AppContext.BaseDirectory)
//                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                          .AddUserSecrets<ReleaseFactory>()
//                          .Build();

//                Configuration = builder.Build();

//                LogConnectionString = Configuration["LogConnectionString"];
//                GenConnectionStringBase = Configuration["GenConnectionStringBase"];

//            }
//            catch
//            {
//                ;
//            }
//        }
//    }
//}
