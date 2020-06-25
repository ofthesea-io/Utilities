namespace FileConverter
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        private static IServiceProvider _serviceProvider;

        #region Methods

        private static async Task Main(string[] args)
        {
            RegisterServices();

            RootCommand command = new RootCommand
            {
                new Option("--i", "Input file") { Argument = new Argument<string>()},
                new Option("--o", "Output file") { Argument = new Argument<string>()},
                new Option("--m", "MetaData e.g. delimiter") { Argument = new Argument<char>()}
            };

            command.Handler = CommandHandler.Create(async (string i, string o, char m) =>
            {
                try
                {
                    IConverter converter = _serviceProvider.GetService<IConverter>();
                    if (m != '\0')
                        converter.MetaData = m;

                    await converter.Process(i, o);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });

            await command.InvokeAsync(args);

            Dispose();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConverter, Converter>();
            services.AddSingleton<IConfiguration, Configuration>();
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void Dispose()
        {
            if (_serviceProvider == null)
                return;

            if (_serviceProvider is IDisposable)
                ((IDisposable)_serviceProvider).Dispose();
        }

        #endregion
    }
}