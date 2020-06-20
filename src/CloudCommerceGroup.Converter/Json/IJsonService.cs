namespace CloudCommerceGroup.Converter.Json
{
    using System.Threading.Tasks;

    public interface IJsonService
    {
        Task<string> ProcessCsvToJson(string[] content, string output, char delimiter);

        Task ProcessJsonToCsv(string input, string output);
    }
}