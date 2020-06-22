namespace FileConverter.Core
{
    using System.ComponentModel;
    using System.Composition.Hosting;
    using System.IO;
    using System.Linq;
    using System.Runtime.Loader;

    public abstract class BaseService
    {
        private ContainerConfiguration _containerConfiguration;

        #region Properties

        public virtual char Delimiter { get; set; } = ',';

        #endregion

        protected void RegisterServices()
        {
            _containerConfiguration = new ContainerConfiguration();
            string path = "C:\\Build\\Plugins\\netcoreapp3.1\\";
            var assemblies = Directory
                .GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

            _containerConfiguration.WithAssemblies(assemblies);
            _containerConfiguration.CreateContainer();
        }
    }
}