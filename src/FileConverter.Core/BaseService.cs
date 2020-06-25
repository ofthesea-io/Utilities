namespace FileConverter.Core
{
    using System;
    using System.Collections.Generic;
    using System.Composition.Hosting;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Loader;

    public abstract class BaseService
    {
        #region Fields

        private ContainerConfiguration _containerConfiguration;

        #endregion

        #region Properties

        public CompositionHost Composition { get; set; }

        public virtual object MetaData { get; set; } = ',';

        #endregion

        #region Methods

        protected void RegisterServices(string pluginDirectory)
        {
            if (string.IsNullOrEmpty(pluginDirectory))
                throw new DirectoryNotFoundException("Plugin directory not found!");

            this._containerConfiguration = new ContainerConfiguration();

            IEnumerable<Assembly> assemblies = Directory
                .GetFiles(pluginDirectory, "*.dll", SearchOption.AllDirectories)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

            this._containerConfiguration.WithAssemblies(assemblies);
            this.Composition = this._containerConfiguration.CreateContainer();
        }

        protected void ParseCsv(ref string[] content, char delimiter)
        {
            if (content.Length == 0)
                throw new NullReferenceException("CSV file is empty");

            int columns = content[0].Split(delimiter).Length;
            if (columns == 1)
                throw new InvalidDataException("Invalid delimiter!");

            for (int i = 1; i < content.Length; i++)
            {
                int j = content[i].Split(delimiter).Length;
                if (j != columns)
                    throw new InvalidDataException("CSV data validation failed!");
            }
        }

        #endregion
    }
}