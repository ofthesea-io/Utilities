namespace CloudCommerceGroup.Converter.Json
{
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class JsonService : IJsonService
    {
        public async Task<string> ProcessCsvToJson(string[] content, string output, char delimiter)
        {
            return await Task.Run(() =>
            {
                var keys = content[0].Split(delimiter);
                var rawObject = content.Skip(1).Select(q => q.Split(delimiter))
                    .Select((x, y) => new {Key = keys[y], Value = y})
                    .ToDictionary(_ => _.Key, _ => _.Value);

                var json = JsonConvert.SerializeObject(rawObject, Formatting.Indented);

                return json;
            });
        }

        public Task ProcessJsonToCsv(string content, string output)
        {
            throw new System.NotImplementedException();
        }
    }
}