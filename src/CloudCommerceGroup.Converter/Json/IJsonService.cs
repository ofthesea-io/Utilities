namespace CloudCommerceGroup.Converter.Json
{
    using System.Threading.Tasks;

    public interface IJsonService
    {
        Task ProcessCsvToJson(string input, string output);

        Task ProcessJsonToCsv(string input, string output);
    }
}