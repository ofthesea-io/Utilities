namespace FileConverter
{
    using System.Threading.Tasks;

    public interface IConverter
    {
        char Delimiter { get; set; }

        Task Process(string input, string output);
    }
}