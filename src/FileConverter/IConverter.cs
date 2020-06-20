namespace FileConverter
{
    using System.Threading.Tasks;

    public interface IConverter
    {
        Task Process(string input, string output, char delimiter);
    }
}