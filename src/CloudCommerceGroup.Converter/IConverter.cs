using System.Threading.Tasks;

namespace CloudCommerceGroup.Converter
{
    public interface IConverter
    {
        string[] ValidateInputFile(string path);

        string GetOutputExtension(string path);

        Task Process(string input, string output, char delimiter);
    }
}