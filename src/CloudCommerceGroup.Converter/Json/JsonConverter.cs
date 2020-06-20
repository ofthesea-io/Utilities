namespace CloudCommerceGroup.Converter.Json
{
    using System.Threading.Tasks;

    using CloudCommerceGroup.Converter.Core;

    public class JsonConverter : Converter, IJsonConverter
    {
        public Task ConvertCsvToJson()
        {
            throw new System.NotImplementedException();
        }

        public Task ConvertJsonToCsv()
        {
            throw new System.NotImplementedException();
        }
    }
}