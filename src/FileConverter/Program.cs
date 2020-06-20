namespace FileConverter
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using Json;
    using Microsoft.Extensions.DependencyInjection;
    using Xml;

    public class Program
    {
        #region Fields

        private static IServiceProvider serviceProvider;

        #endregion

        #region Methods

        private static async Task Main(string[] args)
        {
            Program.RegisterServices();

            var converter = Program.serviceProvider.GetService<IConverter>();

            var command = new RootCommand
            {
                new Option("--i") { Argument = new Argument<string>() },
                new Option("--o") { Argument = new Argument<string>() },
                new Option("--d") { Argument = new Argument<char>() }
            };

            command.Handler = CommandHandler.Create(async (string input, string output, char delimiter) => 
            {
                await converter.Process(input, output, delimiter);
            });

            await command.InvokeAsync(args);

            Program.DisposeServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IXmlService, XmlService>();
            services.AddSingleton<IJsonService, JsonService>();
            services.AddSingleton<IConverter, Converter>();
            Program.serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (Program.serviceProvider == null)
                return;

            if (Program.serviceProvider is IDisposable)
                ((IDisposable)Program.serviceProvider).Dispose();
        }

        #endregion
    }
}