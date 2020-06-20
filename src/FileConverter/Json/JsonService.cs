namespace FileConverter.Json
{
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Newtonsoft.Json;

    public class JsonService : BaseService, IJsonService
    {
        public async Task<string> ProcessCsvToJson(string[] content)
        {
            return await Task.Run(() =>
            {
                var keys = content[0].Split(base.Delimiter);
                var rawObject = content.Skip(1).Select(q => q.Split(base.Delimiter)
                    .Select((x, y) => new { Key = keys[y].Trim(), Value = x })
                    .ToDictionary(_ => _.Key, _ => _.Value));

                var json = JsonConvert.SerializeObject(rawObject, Formatting.Indented);

                return json;
            });
        }

        public Task ProcessJsonToCsv(string content, string output)
        {
            throw new System.NotImplementedException("Currently not implemented");
        }
    }
}