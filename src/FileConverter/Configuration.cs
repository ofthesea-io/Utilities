namespace FileConverter
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class Configuration : IConfiguration
    {
        #region Fields

        private readonly IConfigurationRoot _config;

        #endregion

        #region Constructors

        public Configuration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            this._config = builder.Build();
        }

        #endregion

        public string GetPluginDirectory()
        {
           return this._config.GetSection("PluginDirectory").Value;
        }
    }
}