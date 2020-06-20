namespace CloudCommerceGroup.Converter
{
    using System;

    using CloudCommerceGroup.Converter.Json;
    using CloudCommerceGroup.Converter.Xml;

    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        #region Fields

        private static IServiceProvider serviceProvider;

        #endregion

        #region Methods

        private static void Main(string[] args)
        {
            RegisterServices();

            var converter = serviceProvider.GetService<IConverter>();

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IXmlService, XmlService>();
            services.AddSingleton<IJsonService, JsonService>();
            services.AddSingleton<IConverter, Converter>();
            serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (serviceProvider == null)
                return;

            if (serviceProvider is IDisposable)
                ((IDisposable)serviceProvider).Dispose();
        }

        #endregion
    }
}