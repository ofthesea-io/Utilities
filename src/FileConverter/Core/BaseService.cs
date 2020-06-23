namespace FileConverter.Core
{
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
            this._containerConfiguration = new ContainerConfiguration();

            IEnumerable<Assembly> assemblies = Directory
                .GetFiles(pluginDirectory, "*.dll", SearchOption.TopDirectoryOnly)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

            this._containerConfiguration.WithAssemblies(assemblies);
            this.Composition = this._containerConfiguration.CreateContainer();
        }

        #endregion
    }
}