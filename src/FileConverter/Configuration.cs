namespace FileConverter
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
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


        private void DownloadPlugins(string url)
        {
            string downloadLocation = Path.GetTempPath() + Path.DirectorySeparatorChar + "Converter";
            using (var webClient = new System.Net.WebClient())
            {
                webClient.DownloadFile(new Uri(url), @"c:\temp\myfile.txt");
            }
        }
    }
}