namespace FileConverter.Json
{
    using System.Threading.Tasks;

    public interface IJsonService
    {
        Task<string> ProcessCsvToJson(string[] content);

        Task<string> ProcessJsonToCsv(string input);
    }
}