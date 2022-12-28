using ModelLibrary;
using Serilog;

namespace ApiBackendChallenge.Utility
{
    public class UtilityClass
    {
        public IConfigurationRoot GetSettings()
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.json");

            var config = configuration.Build();

            return config;
        }

        public Serilog.ILogger GetLog()
        {

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(GetSettings()).CreateLogger();
            return Log.Logger;
        }

        public List<Product> FormatLists(List<Product> products)
        {
            products.RemoveAll(x => x.Code <= 0);
            products.RemoveAll(x => x.Name is null);
            products.OrderBy(x => x.Code);
            return products;
        }
    }
}
