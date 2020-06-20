namespace CloudCommerceGroup.Converter.Json
{
    using System.Threading.Tasks;

    public interface IJsonConverter
    {
        Task ConvertCsvToJson();

        Task ConvertJsonToCsv();
    }
}