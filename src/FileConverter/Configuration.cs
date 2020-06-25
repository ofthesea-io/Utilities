namespace FileConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Reflection;
    using Microsoft.Extensions.Configuration;

    public class Configuration : IConfiguration
    {
        #region Fields

        private readonly IConfigurationRoot _config;

        private readonly string _downloadLocation;

        private readonly IConfigurationSection _plugins;

        #endregion

        #region Constructors

        public Configuration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            this._config = builder.Build();

            this._plugins = this._config.GetSection("Plugins");
            this._downloadLocation = Path.GetTempPath() + this._plugins.GetSection("Name").Value;

            #if DEBUG
            this.PluginLocation = this._plugins.GetSection("Debug").Value;
            #else
            this.Init();
            #endif
        }

        #endregion

        #region Properties

        public string PluginLocation { get; private set; }

        #endregion

        #region Methods

        private string GetPluginDirectory()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.PluginLocation = directory + this._plugins.GetSection("Directory").Value;
            return this.PluginLocation;
        }

        private void Init()
        {
            this.CreateDirectory(this._downloadLocation);
            this.CreateDirectory(this.GetPluginDirectory());

            this.DownloadPlugins();
            this.LoadPlugins();
        }


        private void DownloadPlugins()
        {
            try
            {
                IEnumerable<IConfigurationSection> urls = this._plugins.GetSection("Urls").GetChildren();
                using WebClient webClient = new WebClient();
                foreach (IConfigurationSection section in urls)
                {
                    Uri uri = new Uri(section.Value);
                    string downloadName = Path.GetFileName(uri.LocalPath);
                    string download = this._downloadLocation + Path.DirectorySeparatorChar + downloadName;
                    webClient.DownloadFile(uri, download);
                }
            }
            catch (Exception)
            {
                throw new FileNotFoundException("Downloaded file not found!");
            }
        }

        private void LoadPlugins()
        {
            string[] files = Directory.GetFiles(this._downloadLocation);
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                string name = fileInfo.Name;
                if (fileInfo.Name.Contains(".zip"))
                {
                    ZipFile.ExtractToDirectory(fileInfo.FullName, this.PluginLocation);
                }
                else
                {
                    string location = this.PluginLocation + Path.DirectorySeparatorChar + name;
                    fileInfo.MoveTo(location, true);
                }
            }
        }

        private void CreateDirectory(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);

            Directory.CreateDirectory(path);
        }

        #endregion
    }
}