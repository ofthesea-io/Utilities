namespace FileConverter
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;

    public class Program
    {
        #region Methods

        private static async Task Main(string[] args)
        {
            RootCommand command = new RootCommand
            {
                new Option("--i", "Input file") {Argument = new Argument<string>()},
                new Option("--o", "Output file") {Argument = new Argument<string>()},
                new Option("--d", "Delimiter") {Argument = new Argument<char>()}
            };

            command.Handler = CommandHandler.Create(async (string i, string o, char d) =>
            {
                try
                {
                    Converter converter = new Converter();
                    if (d != '\0')
                        converter.Delimiter = d;

                    await converter.Process(i, o);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });

            await command.InvokeAsync(args);
        }

        #endregion
    }
}