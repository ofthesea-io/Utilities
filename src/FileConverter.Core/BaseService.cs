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
        private ContainerConfiguration _containerConfiguration;

        protected CompositionHost Composition { get; private set; }

        public virtual object MetaData { get; set; } = ',';

        protected void ParseCsv(ref string[] content, char delimiter)
        {
            if (content.Length == 0)
            {
                throw new NullReferenceException("CSV file is empty");
            }

            int columns = content[0].Split(delimiter).Length;
            if (columns == 1)
            {
                throw new InvalidDataException("Invalid delimiter!");
            }

            for (var i = 1; i < content.Length; i++)
            {
                int j = content[i].Split(delimiter).Length;
                if (j != columns)
                {
                    throw new InvalidDataException("CSV data validation failed!");
                }
            }
        }

        protected void RegisterServices(string pluginDirectory)
        {
            if (string.IsNullOrEmpty(pluginDirectory))
            {
                throw new DirectoryNotFoundException("Plugin directory not found!");
            }

            _containerConfiguration = new ContainerConfiguration();

            IEnumerable<Assembly> assemblies = Directory.GetFiles(pluginDirectory, "*.dll", SearchOption.AllDirectories)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

            _containerConfiguration.WithAssemblies(assemblies);
            Composition = _containerConfiguration.CreateContainer();
        }
    }
}