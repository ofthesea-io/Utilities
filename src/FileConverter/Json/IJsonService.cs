namespace FileConverter.Json
{
    using System.Threading.Tasks;

    public interface IJsonService
    {
        Task<string> ProcessCsvToJson(string[] content);

        Task ProcessJsonToCsv(string input, string output);
    }
}